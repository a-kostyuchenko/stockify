using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stockify.Common.Infrastructure.Configuration;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Modules.Users.Application.Abstractions;
using Stockify.Modules.Users.Domain.Users;
using Stockify.Modules.Users.Infrastructure.Database;
using Stockify.Modules.Users.Infrastructure.Database.Constants;
using Stockify.Modules.Users.Infrastructure.Database.Repositories;

namespace Stockify.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static void AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
    }
    
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UsersDbContext>((sp, options) => 
            options.UseNpgsql(configuration.GetConnectionStringOrThrow("Database"),
                npgsqlOptions => npgsqlOptions
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());
    }
}
