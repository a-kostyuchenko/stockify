using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Application.Abstractions.Data;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Create;

internal sealed class CreateSessionCommandHandler(
    ISessionFactory sessionFactory,
    ISessionRepository sessionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateSessionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        Result<Session> sessionResult = await sessionFactory.CreateAsync(
            request.IndividualId,
            request.QuestionsCount,
            SessionPolicy.Default,
            cancellationToken);

        if (sessionResult.IsFailure)
        {
            return Result.Failure<Guid>(sessionResult.Error);
        }

        Session session = sessionResult.Value;
        
        sessionRepository.Insert(session);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return session.Id.Value;
    }
}
