using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
    
WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseSerilogRequestLogging();

app.Run();
