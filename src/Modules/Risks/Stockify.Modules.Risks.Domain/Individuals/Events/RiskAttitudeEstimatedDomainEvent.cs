using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals.Events;

public sealed class RiskAttitudeEstimatedDomainEvent(IndividualId individualId) : DomainEvent
{
    public IndividualId IndividualId { get; init; } = individualId;
}
