using Stockify.Modules.Stocks.Domain.Portfolios;

namespace Stockify.Modules.Stocks.Application.Abstractions.Stocks;

public interface IPortfolioOptimizer
{
    PortfolioMetrics Optimize(IReadOnlyCollection<AssetMetrics> assets, InvestmentStrategy strategy);
}
