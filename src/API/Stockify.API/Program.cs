using Asp.Versioning;
using Asp.Versioning.Builder;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Stockify.API.Extensions;
using Stockify.API.Infrastructure;
using Stockify.API.Middleware;
using Stockify.API.OpenApi;
using Stockify.API.OpenTelemetry;
using Stockify.Common.Application;
using Stockify.Common.Infrastructure;
using Stockify.Common.Infrastructure.Configuration;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Modules.Risks.Infrastructure;
using Stockify.Modules.Stocks.Infrastructure;
using Stockify.Modules.Users.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplication([
    Stockify.Modules.Users.Application.AssemblyReference.Assembly,
    Stockify.Modules.Risks.Application.AssemblyReference.Assembly,
    Stockify.Modules.Stocks.Application.AssemblyReference.Assembly
]);

string databaseConnection = builder.Configuration.GetConnectionStringOrThrow("Database");
string redisConnection = builder.Configuration.GetConnectionStringOrThrow("Cache");
string queueConnection = builder.Configuration.GetConnectionStringOrThrow("Queue");

builder.Services.AddInfrastructure(
    DiagnosticConfig.ServiceName,
    [RisksModule.ConfigureConsumers],
    databaseConnection,
    redisConnection,
    queueConnection);

Uri keyCloakHealthUrl = builder.Configuration.GetKeyCloakHealthUrl();

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnection)
    .AddKeyCloak(keyCloakHealthUrl)
    .AddRedis(redisConnection)
    .AddRabbitMQ(rabbitConnectionString: queueConnection);

builder.Configuration.AddModuleConfiguration(["users", "risks", "stocks"]);

builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddRisksModule(builder.Configuration);
builder.Services.AddStocksModule(builder.Configuration);
    
WebApplication app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseBackgroundJobs();

app.UseLogContextTraceLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapEndpoints(versionedGroup);

app.Run();
