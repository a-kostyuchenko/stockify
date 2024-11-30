using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Refit;
using Stockify.Common.Application.EventBus;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Infrastructure.Configuration;
using Stockify.Common.Infrastructure.Extensions;
using Stockify.Common.Infrastructure.Outbox;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Modules.Stocks.Application.Abstractions.Data;
using Stockify.Modules.Stocks.Infrastructure.Database;
using Stockify.Modules.Stocks.Infrastructure.Database.Constants;
using Stockify.Modules.Stocks.Infrastructure.Inbox;
using Stockify.Modules.Stocks.Infrastructure.Outbox;
using Stockify.Modules.Stocks.Infrastructure.Stocks;
using Stockify.Modules.Users.IntegrationEvents;

namespace Stockify.Modules.Stocks.Infrastructure;

public static class StocksModule
{
    public static void AddStocksModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        
        services.AddDomainEventHandlers();
        
        services.AddIntegrationEventHandlers();
        
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        
        services.AddScopedAsMatchingInterfaces(AssemblyReference.Assembly);
        services.AddTransientAsMatchingInterfaces(AssemblyReference.Assembly);
        services.AddSingletonAsMatchingInterfaces(AssemblyReference.Assembly);
    }
    
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StocksDbContext>((sp, options) => 
            options.UseNpgsql(configuration.GetConnectionStringOrThrow("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Stocks))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .UseSnakeCaseNamingConvention());
        
        services.TryAddScoped<IUnitOfWork>(sp => sp.GetRequiredService<StocksDbContext>());
        
        services.Configure<OutboxOptions>(configuration.GetSection(OutboxOptions.ConfigurationSection));
        
        services.Configure<InboxOptions>(configuration.GetSection(InboxOptions.ConfigurationSection));

        services.Configure<AlphavantageOptions>(configuration.GetSection(AlphavantageOptions.ConfigurationSection));
        
        services.TryAddTransient<AlphavantageAuthDelegateHandler>();

        services.AddRefitClient<IAlphavantageClient>()
            .ConfigureHttpClient((sp, httpClient) =>
            {
                AlphavantageOptions options = sp.GetRequiredService<IOptions<AlphavantageOptions>>().Value;
                
                httpClient.BaseAddress = new Uri(options.BaseUrl);
            })
            .AddHttpMessageHandler<AlphavantageAuthDelegateHandler>()
            .AddStandardResilienceHandler();
    }
    
    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>();
    }
    
    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(type => type.IsAssignableTo(typeof(IDomainEventHandler)))
            .ToArray();
    
        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);
    
            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();
    
            Type idempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);
    
            services.Decorate(domainEventHandler, idempotentHandler);
        }
    }
    
    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        Type[] integrationEventHandlers = Presentation.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .ToArray();
    
        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);
    
            Type integrationEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();
    
            Type idempotentHandler =
                typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);
    
            services.Decorate(integrationEventHandler, idempotentHandler);
        }
    }
}
