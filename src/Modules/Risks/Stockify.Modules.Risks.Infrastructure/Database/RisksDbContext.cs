using Microsoft.EntityFrameworkCore;
using Stockify.Modules.Risks.Application.Abstractions.Data;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Risks.Infrastructure.Database;

public sealed class RisksDbContext(DbContextOptions<RisksDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Question> Questions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Risks);
        
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Common.Infrastructure.AssemblyReference.Assembly);
    }
}
