namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetById;

public sealed record SessionResponse(
    Guid Id,
    Guid IndividualId,
    DateTime? StartedAtUtc,
    DateTime? CompletedAtUtc,
    string Status);
