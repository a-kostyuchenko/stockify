namespace Stockify.Modules.Risks.Application.Questions.Queries.Get;

public sealed record GetQuestionsParameters(
    string? SearchTerm,
    int Take,
    int Skip);
