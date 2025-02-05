using Stockify.Common.Application.EventBus;
using Stockify.Common.Application.Exceptions;
using Stockify.Modules.Risks.IntegrationEvents;
using Stockify.Modules.Stocks.Application.Abstractions.Data;
using Stockify.Modules.Stocks.Domain.Stockholders;

namespace Stockify.Modules.Stocks.Presentation.Stockholders;

internal sealed class RiskAttitudeEstimatedIntegrationEventHandler(
    IStockholderRepository stockholderRepository,
    IUnitOfWork unitOfWork) 
    : IntegrationEventHandler<RiskAttitudeEstimatedIntegrationEvent>
{
    public override async Task Handle(
        RiskAttitudeEstimatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Stockholder? stockholder = await stockholderRepository.GetAsync(
            StockholderId.From(integrationEvent.IndividualId),
            cancellationToken);

        if (stockholder is null)
        {
            throw new StockifyException(nameof(IStockholderRepository.GetAsync), StockholderErrors.NotFound);
        }
        
        stockholder.SpecifyRiskProfile(
            integrationEvent.RiskCoefficient,
            integrationEvent.AttitudeType);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
