using Hangfire;
using Hangfire.PostgreSql;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Npgsql;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
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
using Stockify.Common.Infrastructure.EventBus;
using Stockify.Common.Infrastructure.Outbox;

namespace Stockify.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string serviceName,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        string databaseConnection,
        string redisConnection,
        string queueConnection)
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
            
            configuration.UsingRabbitMq((context, cfg) =>
            {

                IOptions<QueueOptions> queueOptions = context.GetRequiredService<IOptions<QueueOptions>>();
                
                cfg.Host(new Uri(queueConnection), host =>
                {
                    host.Username(queueOptions.Value.Username);
                    host.Password(queueOptions.Value.Password);
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        services.TryAddScoped<IEventBus, EventBus.EventBus>();

        services.AddHangfire(config => config.UsePostgreSqlStorage(
            options => options.UseNpgsqlConnection(databaseConnection)));

        services.AddHangfireServer(options =>
            options.SchedulePollingInterval = TimeSpan.FromSeconds(1));

        services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddRedisInstrumentation()
                    .AddHangfireInstrumentation()
                    .AddNpgsql()
                    .AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName);

                tracing.AddOtlpExporter();
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddPrometheusExporter();
            });

        return services;
    }
}
