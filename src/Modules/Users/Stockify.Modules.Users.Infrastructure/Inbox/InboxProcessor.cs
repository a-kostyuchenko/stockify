using System.Data;
using System.Data.Common;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Stockify.Common.Application.Clock;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.EventBus;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Common.Infrastructure.Inbox;
using Stockify.Common.Infrastructure.Serialization;

namespace Stockify.Modules.Users.Infrastructure.Inbox;

internal sealed class InboxProcessor(
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeProvider dateTimeProvider,
    IOptions<InboxOptions> inboxOptions,
    ILogger<InboxProcessor> logger) : IInboxProcessor, IScoped
{
    private const string ModuleName = "Users";
    private readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetryAsync(inboxOptions.Value.MaxRetries,
            attempt => TimeSpan.FromMilliseconds(50 * attempt));
    
    public async Task ProcessAsync()
    {
        logger.LogInformation("{Module} - Beginning to process inbox messages", ModuleName);

        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        await using DbTransaction transaction = await connection.BeginTransactionAsync();

        IReadOnlyList<InboxMessageResponse> inboxMessages = 
            await GetInboxMessagesAsync(connection, transaction);
        
        foreach (InboxMessageResponse inboxMessage in inboxMessages)
        {
            Exception? exception = null;
            
            IIntegrationEvent integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(
                inboxMessage.Content,
                SerializerSettings.Instance)!;

            using IServiceScope scope = serviceScopeFactory.CreateScope();

            IEnumerable<IIntegrationEventHandler> handlers = IntegrationEventHandlersFactory.GetHandlers(
                integrationEvent.GetType(),
                scope.ServiceProvider,
                Presentation.AssemblyReference.Assembly);
            
            // Idempotent consumers allows us to retry processing the same message multiple times
            PolicyResult result = await _retryPolicy.ExecuteAndCaptureAsync(async () =>
            {
                foreach (IIntegrationEventHandler handler in handlers)
                {
                    await handler.Handle(integrationEvent);
                }
            });
            
            if (result.FinalException is not null)
            {
                logger.LogError(
                    result.FinalException,
                    "{Module} - Exception while processing inbox message {MessageId}",
                    ModuleName,
                    inboxMessage.Id);

                exception = result.FinalException;
            }
                
            await UpdateInboxMessageAsync(connection, transaction, inboxMessage, exception);
        }
        
        await transaction.CommitAsync();

        logger.LogInformation("{Module} - Completed processing inbox messages", ModuleName);
    }
    
    private async Task<IReadOnlyList<InboxMessageResponse>> GetInboxMessagesAsync(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        string sql =
            $"""
             SELECT
                id AS {nameof(InboxMessageResponse.Id)},
                content AS {nameof(InboxMessageResponse.Content)}
             FROM users.inbox_messages
             WHERE processed_on_utc IS NULL
             ORDER BY occurred_on_utc
             LIMIT {inboxOptions.Value.BatchSize}
             FOR UPDATE
             """;

        IEnumerable<InboxMessageResponse> inboxMessages = await connection.QueryAsync<InboxMessageResponse>(
            sql,
            transaction: transaction);

        return inboxMessages.AsList();
    }

    private async Task UpdateInboxMessageAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        InboxMessageResponse inboxMessage,
        Exception? exception)
    {
        const string sql =
            $"""
             UPDATE users.inbox_messages
             SET processed_on_utc = @ProcessedOnUtc,
                 error = @Error
             WHERE id = @Id
             """;

        await connection.ExecuteAsync(
            sql,
            new
            {
                inboxMessage.Id,
                ProcessedOnUtc = dateTimeProvider.UtcNow,
                Error = exception?.ToString()
            },
            transaction: transaction);
    }
    
    internal sealed record InboxMessageResponse(Guid Id, string Content);
}
