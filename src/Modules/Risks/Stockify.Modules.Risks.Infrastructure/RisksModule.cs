using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Stockify.Common.Infrastructure.Configuration;
using Stockify.Common.Infrastructure.Outbox;
using Stockify.Modules.Risks.Application.Abstractions.Data;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Infrastructure.Database;
using Stockify.Modules.Risks.Infrastructure.Database.Constants;
using Stockify.Modules.Risks.Infrastructure.Database.Repositories;

namespace Stockify.Modules.Risks.Infrastructure;

public static class RisksModule
{
    public static void AddRisksModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
    }
    
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RisksDbContext>((sp, options) => 
            options.UseNpgsql(configuration.GetConnectionStringOrThrow("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Risks))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .UseSnakeCaseNamingConvention());
        
        services.TryAddScoped<IIndividualRepository, IndividualRepository>();
        
        services.TryAddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RisksDbContext>());
    }
}
