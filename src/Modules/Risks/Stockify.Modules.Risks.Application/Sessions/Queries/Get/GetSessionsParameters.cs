namespace Stockify.Modules.Risks.Application.Sessions.Queries.Get;

public sealed record GetSessionsParameters(
    Guid IndividualId,
    string? Status,
    DateTime? StartedAtUtc,
    DateTime? CompletedAtUtc,
    int Take,
    int Skip);
