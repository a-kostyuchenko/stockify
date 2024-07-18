namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetQuestions;

public sealed record QuestionResponse(
    Guid Id,
    string Content)
{
    public List<AnswerResponse> Answers { get; init; } = [];
};