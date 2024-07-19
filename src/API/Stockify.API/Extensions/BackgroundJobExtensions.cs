using Hangfire;

namespace Stockify.API.Extensions;

public static class BackgroundJobExtensions
{
    public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
    {
        IRecurringJobManager jobClient = app.Services.GetRequiredService<IRecurringJobManager>();
        
        jobClient.AddOrUpdate<Stockify.Modules.Users.Infrastructure.Outbox.IOutboxProcessor>(
            "users-outbox-processor", 
            processor => processor.ProcessAsync(),
            app.Configuration["Users:Outbox:Schedule"]);
        
        jobClient.AddOrUpdate<Stockify.Modules.Users.Infrastructure.Inbox.IInboxProcessor>(
            "users-inbox-processor", 
            processor => processor.ProcessAsync(),
            app.Configuration["Users:Inbox:Schedule"]);
        
        
        jobClient.AddOrUpdate<Stockify.Modules.Risks.Infrastructure.Outbox.IOutboxProcessor>(
            "risks-outbox-processor", 
            processor => processor.ProcessAsync(),
            app.Configuration["Risks:Outbox:Schedule"]);
        
        jobClient.AddOrUpdate<Stockify.Modules.Risks.Infrastructure.Inbox.IInboxProcessor>(
            "risks-inbox-processor", 
            processor => processor.ProcessAsync(),
            app.Configuration["Risks:Inbox:Schedule"]);
        
        jobClient.AddOrUpdate<Stockify.Modules.Stocks.Infrastructure.Outbox.IOutboxProcessor>(
            "stocks-outbox-processor", 
            processor => processor.ProcessAsync(),
            app.Configuration["Stocks:Outbox:Schedule"]);
        
        jobClient.AddOrUpdate<Stockify.Modules.Stocks.Infrastructure.Inbox.IInboxProcessor>(
            "stocks-inbox-processor", 
            processor => processor.ProcessAsync(),
            app.Configuration["Stocks:Inbox:Schedule"]);
        
        return app;
    }
}
