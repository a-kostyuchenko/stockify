using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Stockify.Common.Application.EventBus;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Infrastructure.Configuration;
using Stockify.Common.Infrastructure.Extensions;
using Stockify.Common.Infrastructure.Outbox;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Modules.Risks.Application.Abstractions.Data;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Sessions;
using Stockify.Modules.Risks.Infrastructure.Database;
using Stockify.Modules.Risks.Infrastructure.Database.Constants;
using Stockify.Modules.Risks.Infrastructure.Inbox;
using Stockify.Modules.Risks.Infrastructure.Outbox;
using Stockify.Modules.Users.IntegrationEvents;

namespace Stockify.Modules.Risks.Infrastructure;

public static class RisksModule
{
    public static void AddRisksModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        
        services.AddDomainEventHandlers();
        
        services.AddIntegrationEventHandlers();
        
        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
        
        services.AddDomainServices();
        
        services.AddScopedAsMatchingInterfaces(AssemblyReference.Assembly);
        services.AddTransientAsMatchingInterfaces(AssemblyReference.Assembly);
        services.AddSingletonAsMatchingInterfaces(AssemblyReference.Assembly);
    }
    
    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>();
    }
    
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RisksDbContext>((sp, options) => 
            options.UseNpgsql(configuration.GetConnectionStringOrThrow("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Risks))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .UseSnakeCaseNamingConvention());
        
        services.TryAddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RisksDbContext>());
        
        services.Configure<OutboxOptions>(configuration.GetSection(OutboxOptions.ConfigurationSection));
        
        services.Configure<InboxOptions>(configuration.GetSection(InboxOptions.ConfigurationSection));
    }

    private static void AddDomainServices(this IServiceCollection services)
    {
        services.TryAddScoped<ISessionFactory, SessionFactory>();
        
        services.TryAddScoped<ICalculator, RiskCalculator>();
        
        services.TryAddSingleton<IFormulaSelector, FormulaSelector>();
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
