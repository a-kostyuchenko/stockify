using Microsoft.EntityFrameworkCore;
using Stockify.Modules.Risks.Infrastructure.Database;
using Stockify.Modules.Users.Infrastructure.Database;

namespace Stockify.API.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<UsersDbContext>(scope);
        ApplyMigration<RisksDbContext>(scope);
    }
    
    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
