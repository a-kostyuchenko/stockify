using Hangfire;
using Hangfire.PostgreSql;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;
using Stockify.Common.Application.Caching;
using Stockify.Common.Application.Clock;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.EventBus;
using Stockify.Common.Infrastructure.Authentication;
using Stockify.Common.Infrastructure.Authorization;
using Stockify.Common.Infrastructure.Caching;
using Stockify.Common.Infrastructure.Clock;
using Stockify.Common.Infrastructure.Data;
using Stockify.Common.Infrastructure.Outbox;

namespace Stockify.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        string databaseConnection,
        string redisConnection)
    {
        services.AddAuthenticationInternal();

        services.AddAuthorizationInternal();
        
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();
        
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

        services.AddMassTransit(configuration =>
        {
            foreach (Action<IRegistrationConfigurator> configureConsumer in moduleConfigureConsumers)
            {
                configureConsumer(configuration);
            }
            configuration.SetKebabCaseEndpointNameFormatter();
            
            configuration.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
        
        services.TryAddScoped<IEventBus, EventBus.EventBus>();

        services.AddHangfire(config => config.UsePostgreSqlStorage(
            options => options.UseNpgsqlConnection(databaseConnection)));

        services.AddHangfireServer(options =>
            options.SchedulePollingInterval = TimeSpan.FromSeconds(1));

        return services;
    }
}
