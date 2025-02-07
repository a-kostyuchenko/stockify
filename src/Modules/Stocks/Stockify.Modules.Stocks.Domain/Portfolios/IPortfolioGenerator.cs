using Stockify.Modules.Stocks.Domain.Stockholders;

namespace Stockify.Modules.Stocks.Domain.Portfolios;

public interface IPortfolioGenerator
{
    Task<Portfolio> GenerateAsync(
        Stockholder stockholder,
        CancellationToken cancellationToken = default);
}
