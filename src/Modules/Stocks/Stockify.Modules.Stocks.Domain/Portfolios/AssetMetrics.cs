using Stockify.Modules.Stocks.Domain.Tickers;

namespace Stockify.Modules.Stocks.Domain.Portfolios;

public sealed record AssetMetrics(
    Symbol Symbol,
    decimal ExpectedReturn,
    decimal Volatility,
    Dictionary<Symbol, decimal> Correlations);
