namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetQuestions;

public sealed record GetSessionQuestionsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<QuestionResponse> Questions);