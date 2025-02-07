using Stockify.Modules.Stocks.Domain.Tickers;

namespace Stockify.Modules.Stocks.Domain.Portfolios;

public sealed record AllocationEntry(Symbol Symbol, decimal Percentage);