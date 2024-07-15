using Stockify.Modules.Risks.Domain.Individuals;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.Get;

public sealed record GetSessionsParameters(
    Guid IndividualId,
    string? Status,
    DateTime? StartedAtUtc,
    int Take,
    int Skip);
