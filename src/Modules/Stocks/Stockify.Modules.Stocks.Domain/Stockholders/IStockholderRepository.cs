namespace Stockify.Modules.Stocks.Domain.Stockholders;

public interface IStockholderRepository
{
    Task<Stockholder?> GetAsync(StockholderId stockholderId, CancellationToken cancellationToken = default);
    void Insert(Stockholder stockholder);
}
