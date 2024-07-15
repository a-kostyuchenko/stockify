using Stockify.Modules.Risks.Application.Sessions.Queries.GetById;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.Get;

public sealed record GetSessionsResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<SessionResponse> Sessions);
