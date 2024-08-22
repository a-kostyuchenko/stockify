using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;

namespace Stockify.Modules.Stocks.Application.Stocks.Queries.GetMarketStatus;

internal sealed class GetMarketStatusQueryHandler(IStocksService stocksService) 
    : IQueryHandler<GetMarketStatusQuery, List<MarketResponse>>
{
    public async Task<Result<List<MarketResponse>>> Handle(
        GetMarketStatusQuery request,
        CancellationToken cancellationToken) =>
        await stocksService
            .GetGlobalMarketStatusAsync(cancellationToken);
}
