using Stockify.Modules.Stocks.Domain.Portfolios;
using Stockify.Modules.Stocks.Domain.Tickers;

namespace Stockify.Modules.Stocks.Application.Abstractions.Stocks;

public interface IMarketDataProvider
{
    Task<IReadOnlyCollection<AssetMetrics>> GetAssetsMetricsAsync(
        IEnumerable<Symbol> tickerIds,
        CancellationToken cancellationToken = default);
}
