namespace Stockify.Modules.Risks.Application.Questions.Queries.GetById;

public sealed record QuestionResponse(Guid Id, string Content)
{
    public List<AnswerResponse> Answers { get; } = [];
}
