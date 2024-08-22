using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Application.Abstractions.Stocks;

public interface IStocksService
{
    Task<Result<QuoteResponse>> GetQuoteAsync(
        string symbol,
        CancellationToken cancellationToken = default);
    Task<Result<List<MarketResponse>>> GetGlobalMarketStatusAsync(
        CancellationToken cancellationToken = default);
}
