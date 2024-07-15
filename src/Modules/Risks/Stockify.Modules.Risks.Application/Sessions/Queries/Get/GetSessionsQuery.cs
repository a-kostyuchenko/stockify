using Stockify.Common.Application.Messaging;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.Get;

public sealed record GetSessionsQuery(
    string? Status,
    DateTime? StartedAtUtc,
    int Page,
    int PageSize) : IQuery<GetSessionsResponse>;
