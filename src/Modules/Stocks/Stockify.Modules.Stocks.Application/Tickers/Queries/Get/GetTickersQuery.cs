using Stockify.Common.Application.Messaging;
using Stockify.Common.Application.Pagination;

namespace Stockify.Modules.Stocks.Application.Tickers.Queries.Get;

public sealed record GetTickersQuery(
    string? SearchTerm,
    int Page,
    int PageSize) : IQuery<PagedResponse<TickerResponse>>;
