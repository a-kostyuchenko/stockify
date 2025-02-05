using Microsoft.EntityFrameworkCore;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Modules.Stocks.Domain.Stockholders;

namespace Stockify.Modules.Stocks.Infrastructure.Database.Repositories;

internal sealed class StockholderRepository(StocksDbContext context) : IStockholderRepository, IScoped
{
    public async Task<Stockholder?> GetAsync(
        StockholderId stockholderId,
        CancellationToken cancellationToken = default) =>
        await context.Stockholders.FirstOrDefaultAsync(s => s.Id == stockholderId, cancellationToken);

    public void Insert(Stockholder stockholder) => 
        context.Stockholders.Add(stockholder);
}
