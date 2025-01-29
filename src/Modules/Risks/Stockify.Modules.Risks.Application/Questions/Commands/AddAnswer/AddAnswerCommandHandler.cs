using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Application.Abstractions.Data;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Application.Questions.Commands.AddAnswer;

internal sealed class AddAnswerCommandHandler(
    IQuestionRepository questionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddAnswerCommand>
{
    public async Task<Result> Handle(AddAnswerCommand request, CancellationToken cancellationToken)
    {
        Question? question = await questionRepository.GetAsync(request.QuestionId, cancellationToken);
        
        if (question is null)
        {
            return Result.Failure(QuestionErrors.NotFound);
        }

        Result result = question.AddAnswer(
            request.Content,
            request.Points,
            request.Explanation);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
