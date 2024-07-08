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
using Stockify.Common.Infrastructure.EventBus;
using Stockify.Common.Presentation.Endpoints;
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
    Stockify.Modules.Users.Application.AssemblyReference.Assembly
]);

string databaseConnection = builder.Configuration.GetConnectionStringOrThrow("Database");
string redisConnection = builder.Configuration.GetConnectionStringOrThrow("Cache");
var queueSettings = new QueueSettings(
    builder.Configuration.GetConnectionStringOrThrow("Queue"),
    builder.Configuration.GetValueOrThrow<string>("Queue:Username")!,
    builder.Configuration.GetValueOrThrow<string>("Queue:Password")!);

builder.Services.AddInfrastructure(
    DiagnosticConfig.ServiceName,
    [],
    databaseConnection,
    redisConnection,
    queueSettings);

Uri keyCloakHealthUrl = builder.Configuration.GetKeyCloakHealthUrl();

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnection)
    .AddKeyCloak(keyCloakHealthUrl);

builder.Configuration.AddModuleConfiguration(["users"]);

builder.Services.AddUsersModule(builder.Configuration);
    
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
