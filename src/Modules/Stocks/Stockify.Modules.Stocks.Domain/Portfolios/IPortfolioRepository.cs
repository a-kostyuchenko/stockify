using Stockify.Modules.Stocks.Domain.Stockholders;

namespace Stockify.Modules.Stocks.Domain.Portfolios;

public interface IPortfolioRepository
{
    Task<Portfolio?> GetAsync(PortfolioId id, CancellationToken cancellationToken = default);
    
    Task<Portfolio?> GetLatestByStockholderAsync(
        StockholderId stockholderId,
        CancellationToken cancellationToken = default);
    
    void Insert(Portfolio portfolio);
}