using Stockify.Common.Application.Exceptions;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Application.Abstractions.Data;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Sessions;
using Stockify.Modules.Risks.Domain.Sessions.Events;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Complete;

internal sealed class SessionCompletedDomainEventHandler(
    ISessionRepository sessionRepository,
    IIndividualRepository individualRepository,
    IUnitOfWork unitOfWork,
    ICalculator calculator) : DomainEventHandler<SessionCompletedDomainEvent>
{
    public override async Task Handle(
        SessionCompletedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Session? session = await sessionRepository.GetAsync(domainEvent.SessionId, cancellationToken);

        if (session is null)
        {
            throw new StockifyException(nameof(SessionCompletedDomainEvent), SessionErrors.NotFound);
        }
        
        Individual? individual = await individualRepository.GetAsync(session.IndividualId, cancellationToken);

        if (individual is null)
        {
            throw new StockifyException(nameof(SessionCompletedDomainEvent), IndividualErrors.NotFound);
        }
        
        Result<RiskAttitude> attitudeResult = calculator.Calculate(session, null!);

        if (attitudeResult.IsFailure)
        {
            throw new StockifyException(nameof(SessionCompletedDomainEvent), attitudeResult.Error);
        }
        
        individual.SpecifyAttitude(attitudeResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
