using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Modules.Stocks.Application.Abstractions.Portfolios;
using Stockify.Modules.Stocks.Domain.Portfolios;

namespace Stockify.Modules.Stocks.Infrastructure.Portfolios;

internal sealed class PortfolioOptimizer
    : IPortfolioOptimizer, IScoped
{
    public PortfolioMetrics Optimize(
        IReadOnlyList<AssetMetrics> assets,
        InvestmentStrategy strategy)
    {
        throw new NotImplementedException();
    }
}
