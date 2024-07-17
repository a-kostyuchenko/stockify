using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Application.Abstractions.Data;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.SubmitAnswer;

internal sealed class SubmitAnswerCommandHandler(
    ISessionRepository sessionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<SubmitAnswerCommand>
{
    public async Task<Result> Handle(SubmitAnswerCommand request, CancellationToken cancellationToken)
    {
        Session? session = await sessionRepository.GetAsync(request.SessionId, cancellationToken);

        if (session is null)
        {
            return Result.Failure(SessionErrors.NotFound);
        }
        
        Result submitResult = session.SubmitAnswer(request.QuestionId, request.AnswerId);

        if (submitResult.IsFailure)
        {
            return submitResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
