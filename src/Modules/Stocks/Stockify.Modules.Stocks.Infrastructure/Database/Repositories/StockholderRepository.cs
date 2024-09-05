using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Modules.Stocks.Domain.Stockholders;

namespace Stockify.Modules.Stocks.Infrastructure.Database.Repositories;

internal sealed class StockholderRepository(StocksDbContext context) : IStockholderRepository, IScoped
{
    public void Insert(Stockholder stockholder) => 
        context.Stockholders.Add(stockholder);
}
