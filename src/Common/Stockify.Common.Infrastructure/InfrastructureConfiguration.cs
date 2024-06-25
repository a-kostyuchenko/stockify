using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Stockify.Common.Application.Clock;
using Stockify.Common.Application.Data;
using Stockify.Common.Infrastructure.Authentication;
using Stockify.Common.Infrastructure.Clock;
using Stockify.Common.Infrastructure.Data;

namespace Stockify.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string databaseConnection)
    {
        services.AddAuthenticationInternal();
        
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnection).Build();
        services.TryAddSingleton(npgsqlDataSource);
        
        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();

        return services;
    }
}
