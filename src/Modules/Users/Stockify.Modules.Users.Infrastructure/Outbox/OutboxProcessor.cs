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
using Stockify.Common.Application.Messaging;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Common.Domain;
using Stockify.Common.Infrastructure.Outbox;
using Stockify.Common.Infrastructure.Serialization;

namespace Stockify.Modules.Users.Infrastructure.Outbox;

internal sealed class OutboxProcessor(
    IDbConnectionFactory dbConnectionFactory,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeProvider dateTimeProvider,
    IOptions<OutboxOptions> outboxOptions,
    ILogger<OutboxProcessor> logger) : IOutboxProcessor, IScoped
{
    private const string ModuleName = "Users";
    private readonly AsyncRetryPolicy _retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetryAsync(outboxOptions.Value.MaxRetries,
            attempt => TimeSpan.FromMilliseconds(50 * attempt));
    
    public async Task ProcessAsync()
    {
        logger.LogInformation("{Module} - Beginning to process outbox messages", ModuleName);

        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        await using DbTransaction transaction = await connection.BeginTransactionAsync();
        
        IReadOnlyCollection<OutboxMessageResponse> outboxMessages = 
            await GetOutboxMessagesAsync(connection, transaction);

        foreach (OutboxMessageResponse outboxMessage in outboxMessages)
        {
            Exception? exception = null;
            
            IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                outboxMessage.Content,
                SerializerSettings.Instance)!;
            
            using IServiceScope scope = serviceScopeFactory.CreateScope();
            
            IEnumerable<IDomainEventHandler> handlers = DomainEventHandlersFactory.GetHandlers(
                domainEvent.GetType(),
                scope.ServiceProvider,
                Application.AssemblyReference.Assembly);
            
            // Idempotent consumers allows us to retry processing the same message multiple times
            PolicyResult result = await _retryPolicy.ExecuteAndCaptureAsync(async () =>
            {
                foreach (IDomainEventHandler handler in handlers)
                {
                    await handler.Handle(domainEvent);
                }
            });

            if (result.FinalException is not null)
            {
                logger.LogError(
                    result.FinalException,
                    "{Module} - Exception while processing outbox message {MessageId}",
                    ModuleName,
                    outboxMessage.Id);

                exception = result.FinalException;
            }
            
            await UpdateOutboxMessageAsync(connection, transaction, outboxMessage, exception);
        }
        
        await transaction.CommitAsync();

        logger.LogInformation("{Module} - Completed processing outbox messages", ModuleName);
    }
    
    private async Task<IReadOnlyList<OutboxMessageResponse>> GetOutboxMessagesAsync(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        string sql =
            $"""
             SELECT
                id AS {nameof(OutboxMessageResponse.Id)},
                content AS {nameof(OutboxMessageResponse.Content)}
             FROM users.outbox_messages
             WHERE processed_on_utc IS NULL
             ORDER BY occurred_on_utc
             LIMIT {outboxOptions.Value.BatchSize}
             FOR UPDATE SKIP LOCKED 
             """;

        IEnumerable<OutboxMessageResponse> outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(
            sql,
            transaction: transaction);

        return outboxMessages.ToList();
    }

    private async Task UpdateOutboxMessageAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        OutboxMessageResponse outboxMessage,
        Exception? exception)
    {
        const string sql =
            $"""
            UPDATE users.outbox_messages
            SET processed_on_utc = @ProcessedOnUtc,
                error = @Error
            WHERE id = @Id
            """;

        await connection.ExecuteAsync(
            sql,
            new
            {
                outboxMessage.Id,
                ProcessedOnUtc = dateTimeProvider.UtcNow,
                Error = exception?.ToString()
            },
            transaction: transaction);
    }
    
    internal sealed record OutboxMessageResponse(Guid Id, string Content);
}
