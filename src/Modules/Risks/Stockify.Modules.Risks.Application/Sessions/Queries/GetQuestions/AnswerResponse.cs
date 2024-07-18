namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetQuestions;

public sealed record AnswerResponse(
    Guid AnswerId,
    string Content);