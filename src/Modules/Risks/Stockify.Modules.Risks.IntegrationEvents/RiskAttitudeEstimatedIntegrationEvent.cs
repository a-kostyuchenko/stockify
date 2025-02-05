using Stockify.Common.Application.EventBus;

namespace Stockify.Modules.Risks.IntegrationEvents;

public sealed class RiskAttitudeEstimatedIntegrationEvent(
    Guid id,
    DateTime occurredOnUtc,
    Guid individualId,
    decimal riskCoefficient,
    string attitudeType) : IntegrationEvent(id, occurredOnUtc)
{
    public Guid IndividualId { get; init; } = individualId;
    public decimal RiskCoefficient { get; init; } = riskCoefficient;
    public string AttitudeType { get; init; } = attitudeType;
}
