using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Stocks;

namespace Stockify.Modules.Stocks.Application.Stocks.Queries.GetStocksData;

internal sealed class GetStocksDataQueryHandler(IStocksService stocksService) 
    : IQueryHandler<GetStocksDataQuery, List<TimeSeriesResponse>>
{
    public async Task<Result<List<TimeSeriesResponse>>> Handle(
        GetStocksDataQuery request,
        CancellationToken cancellationToken) =>
        await stocksService.GetStocksData(request.Symbol, cancellationToken);
}
