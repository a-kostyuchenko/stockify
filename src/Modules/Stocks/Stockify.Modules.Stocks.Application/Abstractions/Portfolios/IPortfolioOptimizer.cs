using Stockify.Modules.Stocks.Domain.Portfolios;

namespace Stockify.Modules.Stocks.Application.Abstractions.Portfolios;

public interface IPortfolioOptimizer
{
    PortfolioMetrics Optimize(IReadOnlyList<AssetMetrics> assets, InvestmentStrategy strategy);
}
