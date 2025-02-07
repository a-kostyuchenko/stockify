namespace Stockify.Modules.Stocks.Domain.Portfolios;

public sealed class PortfolioMetrics
{
    public decimal ExpectedReturn { get; init; }
    public decimal Risk { get; init; }
    public decimal SharpeRatio { get; init; }
    public IReadOnlyCollection<AllocationEntry> Allocations { get; init; } = [];
}