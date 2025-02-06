using Microsoft.EntityFrameworkCore;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Modules.Stocks.Domain.Tickers;

namespace Stockify.Modules.Stocks.Infrastructure.Database.Repositories;

internal sealed class TickerRepository(StocksDbContext context) : ITickerRepository, IScoped
{
    public async Task<Ticker?> GetAsync(Symbol id, CancellationToken cancellationToken = default) => 
        await context.Tickers.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public void Insert(Ticker ticker) => 
        context.Tickers.Add(ticker);
}
