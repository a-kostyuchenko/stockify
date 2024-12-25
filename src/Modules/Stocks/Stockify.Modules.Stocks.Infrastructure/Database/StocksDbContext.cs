using Microsoft.EntityFrameworkCore;
using Stockify.Modules.Stocks.Application.Abstractions.Data;
using Stockify.Modules.Stocks.Domain.Stockholders;
using Stockify.Modules.Stocks.Domain.Subscriptions;
using Stockify.Modules.Stocks.Domain.Tickers;
using Stockify.Modules.Stocks.Domain.TickerTypes;
using Stockify.Modules.Stocks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Stocks.Infrastructure.Database;

public sealed class StocksDbContext(DbContextOptions<StocksDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Stockholder> Stockholders { get; set; }
    public DbSet<Ticker> Tickers { get; set; }
    public DbSet<TickerType> TickerTypes { get; set; }
    public DbSet<TickerSubscription> TickerSubscriptions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Stocks);
        
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Common.Infrastructure.AssemblyReference.Assembly);
    }
}
