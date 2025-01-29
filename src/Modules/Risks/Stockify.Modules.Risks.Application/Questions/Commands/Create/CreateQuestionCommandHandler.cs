using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Application.Abstractions.Data;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Application.Questions.Commands.Create;

internal sealed class CreateQuestionCommandHandler(
    IQuestionRepository questionRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateQuestionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = Question.Create(
            request.Content,
            QuestionCategory.FromName(request.Category)!,
            request.Weight);
        
        questionRepository.Insert(question);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return question.Id.Value;
    }
}
