using Stockify.Common.Application.EventBus;
using Stockify.Common.Application.Exceptions;
using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Individuals.Events;
using Stockify.Modules.Risks.IntegrationEvents;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Complete;

internal sealed class RiskAttitudeEstimatedDomainEventHandler(
    IIndividualRepository individualRepository,
    IEventBus eventBus) : DomainEventHandler<RiskAttitudeEstimatedDomainEvent>
{
    public override async Task Handle(
        RiskAttitudeEstimatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Individual? individual = await individualRepository.GetAsync(
            domainEvent.IndividualId,
            cancellationToken);

        if (individual is null)
        {
            throw new StockifyException(nameof(IIndividualRepository.GetAsync), IndividualErrors.NotFound);
        }

        await eventBus.PublishAsync(
            new RiskAttitudeEstimatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                individual.Id.Value,
                individual.Attitude.Coefficient,
                individual.Attitude.Type.Name),
            cancellationToken);
    }
}
