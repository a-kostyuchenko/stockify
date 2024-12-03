using Microsoft.EntityFrameworkCore;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Modules.Stocks.Domain.TickerTypes;

namespace Stockify.Modules.Stocks.Infrastructure.Database.Repositories;

internal sealed class TickerTypeRepository(StocksDbContext context) : ITickerTypeRepository, IScoped
{
    public async Task<TickerType?> GetAsync(TickerTypeId tickerTypeId, CancellationToken cancellationToken = default) => 
        await context.TickerTypes.FirstOrDefaultAsync(t => t.Id == tickerTypeId, cancellationToken);

    public void Insert(TickerType tickerType) => 
        context.TickerTypes.Add(tickerType);
}
