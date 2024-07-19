using System.Data.Common;
using Dapper;
using MassTransit;
using Newtonsoft.Json;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.EventBus;
using Stockify.Common.Infrastructure.Inbox;
using Stockify.Common.Infrastructure.Serialization;

namespace Stockify.Modules.Stocks.Infrastructure.Inbox;

internal sealed class IntegrationEventConsumer<TIntegrationEvent>(
    IDbConnectionFactory dbConnectionFactory) : IConsumer<TIntegrationEvent>
where TIntegrationEvent : IntegrationEvent
{
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        TIntegrationEvent integrationEvent = context.Message;

        var inboxMessage = new InboxMessage
        {
            Id = integrationEvent.Id,
            Type = integrationEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(integrationEvent, SerializerSettings.Instance),
            OccurredOnUtc = integrationEvent.OccurredOnUtc
        };
        
        const string sql =
            """
            INSERT INTO stocks.inbox_messages(id, type, content, occurred_on_utc)
            VALUES (@Id, @Type, @Content::json, @OccurredOnUtc)
            """;

        await connection.ExecuteAsync(sql, inboxMessage);
    }
}
