using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;
using Stockify.Common.Application.Caching;
using Stockify.Common.Application.Clock;
using Stockify.Common.Application.Data;
using Stockify.Common.Infrastructure.Authentication;
using Stockify.Common.Infrastructure.Authorization;
using Stockify.Common.Infrastructure.Caching;
using Stockify.Common.Infrastructure.Clock;
using Stockify.Common.Infrastructure.Data;

namespace Stockify.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string databaseConnection,
        string redisConnection)
    {
        services.AddAuthenticationInternal();

        services.AddAuthorizationInternal();
        
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnection).Build();
        services.TryAddSingleton(npgsqlDataSource);
        
        services.TryAddScoped<IDbConnectionFactory, DbConnectionFactory>();
        
        try
        {
            IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnection);
            services.AddSingleton(connectionMultiplexer);
            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));
        }
        catch
        {
            services.AddDistributedMemoryCache();
        }

        services.TryAddSingleton<ICacheService, CacheService>();

        return services;
    }
}
