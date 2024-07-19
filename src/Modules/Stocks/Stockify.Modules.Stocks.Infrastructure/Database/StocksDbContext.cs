using Microsoft.EntityFrameworkCore;
using Stockify.Modules.Stocks.Application.Abstractions.Data;
using Stockify.Modules.Stocks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Stocks.Infrastructure.Database;

public sealed class StocksDbContext(DbContextOptions<StocksDbContext> options) : DbContext(options), IUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Stocks);
        
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Common.Infrastructure.AssemblyReference.Assembly);
    }
}
