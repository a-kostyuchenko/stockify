using Stockify.Modules.Stocks.Domain.Portfolios;
using Stockify.Modules.Stocks.Domain.Tickers;

namespace Stockify.Modules.Stocks.Application.Abstractions.Portfolios;

public interface IMarketDataProvider
{
    Task<IReadOnlyList<AssetMetrics>> GetAssetsMetricsAsync(
        IEnumerable<Symbol> tickerIds,
        CancellationToken cancellationToken = default);
}
