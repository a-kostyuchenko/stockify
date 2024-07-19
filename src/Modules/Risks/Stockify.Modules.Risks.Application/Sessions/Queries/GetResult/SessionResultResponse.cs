namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetResult;

public sealed record SessionResultResponse(
    Guid SessionId,
    DateTime StartedAtUtc,
    DateTime CompletedAtUtc,
    int TotalPoints,
    int MaxPoints);