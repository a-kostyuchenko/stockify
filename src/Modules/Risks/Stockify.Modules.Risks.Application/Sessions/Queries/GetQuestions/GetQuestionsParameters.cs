namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetQuestions;

public sealed record GetQuestionsParameters(
    Guid SessionId,
    Guid IndividualId,
    int Take,
    int Skip);
