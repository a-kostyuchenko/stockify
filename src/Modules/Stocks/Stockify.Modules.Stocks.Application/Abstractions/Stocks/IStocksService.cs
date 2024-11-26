using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Application.Abstractions.Stocks;

public interface IStocksService
{
    Task<Result<QuoteResponse>> GetQuoteAsync(
        string ticker,
        CancellationToken cancellationToken = default);
    Task<Result<List<MarketResponse>>> GetGlobalMarketStatusAsync(
        CancellationToken cancellationToken = default);

    Task<Result<List<TimeSeriesResponse>>> GetStocksData(
        string ticker,
        CancellationToken cancellationToken = default);
}
