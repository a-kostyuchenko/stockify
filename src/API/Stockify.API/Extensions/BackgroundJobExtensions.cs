using Hangfire;
using Stockify.Modules.Users.Infrastructure.Inbox;
using Stockify.Modules.Users.Infrastructure.Outbox;

namespace Stockify.API.Extensions;

public static class BackgroundJobExtensions
{
    public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
    {
        IRecurringJobManager jobClient = app.Services.GetRequiredService<IRecurringJobManager>();
        
        jobClient.AddOrUpdate<IOutboxProcessor>(
            "users-outbox-processor", 
            processor => processor.ProcessAsync(),
            app.Configuration["Users:Outbox:Schedule"]);
        
        jobClient.AddOrUpdate<IInboxProcessor>(
            "users-inbox-processor", 
            processor => processor.ProcessAsync(),
            app.Configuration["Users:Inbox:Schedule"]);
        
        return app;
    }
}
