using Microsoft.EntityFrameworkCore;
using Stockify.Modules.Users.Application.Abstractions;
using Stockify.Modules.Users.Domain.Users;
using Stockify.Modules.Users.Infrastructure.Database.Constants;

namespace Stockify.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
            
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
