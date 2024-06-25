using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Stockify.API.Extensions;
using Stockify.API.Infrastructure;
using Stockify.Common.Application;
using Stockify.Common.Infrastructure;
using Stockify.Common.Infrastructure.Configuration;
using Stockify.Modules.Users.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication([
    Stockify.Modules.Users.Application.AssemblyReference.Assembly
]);

string databaseConnection = builder.Configuration.GetConnectionStringOrThrow("Database");

builder.Services.AddInfrastructure(databaseConnection);

Uri keyCloakHealthUrl = builder.Configuration.GetKeyCloakHealthUrl();

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnection)
    .AddKeyCloak(keyCloakHealthUrl);

builder.Services.AddUsersModule(builder.Configuration);
    
WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.Run();
