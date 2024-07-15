namespace Stockify.Modules.Risks.Application.Questions.Queries.Get;

public sealed record GetQuestionsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<QuestionResponse> Questions);
