using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Individuals;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.Get;

public sealed record GetSessionsQuery(
    IndividualId IndividualId,
    string? Status,
    DateTime? StartedAtUtc,
    DateTime? CompletedAtUtc,
    int Page,
    int PageSize) : IQuery<GetSessionsResponse>;
