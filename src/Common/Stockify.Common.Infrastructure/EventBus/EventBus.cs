using MassTransit;
using Stockify.Common.Application.EventBus;

namespace Stockify.Common.Infrastructure.EventBus;

internal sealed class EventBus(IBus bus) : IEventBus
{
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) 
        where T : IIntegrationEvent =>
        await bus.Publish(integrationEvent, cancellationToken);
}
