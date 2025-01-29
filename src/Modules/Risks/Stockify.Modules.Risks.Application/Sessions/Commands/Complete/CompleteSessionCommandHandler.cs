using Stockify.Common.Application.Clock;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Application.Abstractions.Data;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Complete;

internal sealed class CompleteSessionCommandHandler(
    ISessionRepository sessionRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CompleteSessionCommand>
{
    public async Task<Result> Handle(CompleteSessionCommand request, CancellationToken cancellationToken)
    {
        Session? session = await sessionRepository.GetAsync(request.SessionId, cancellationToken);

        if (session is null)
        {
            return Result.Failure(SessionErrors.NotFound);
        }
        
        Result result = session.Complete(dateTimeProvider.UtcNow, SessionPolicy.Default);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
