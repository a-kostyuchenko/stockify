using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Stockify.Common.Application.Authorization;
using Stockify.Common.Application.EventBus;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Infrastructure.Configuration;
using Stockify.Common.Infrastructure.Outbox;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Modules.Users.Application.Abstractions.Data;
using Stockify.Modules.Users.Application.Abstractions.Identity;
using Stockify.Modules.Users.Domain.Users;
using Stockify.Modules.Users.Infrastructure.Authentication;
using Stockify.Modules.Users.Infrastructure.Authorization;
using Stockify.Modules.Users.Infrastructure.Database;
using Stockify.Modules.Users.Infrastructure.Database.Constants;
using Stockify.Modules.Users.Infrastructure.Database.Repositories;
using Stockify.Modules.Users.Infrastructure.Identity;
using Stockify.Modules.Users.Infrastructure.Inbox;
using Stockify.Modules.Users.Infrastructure.Outbox;

namespace Stockify.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static void AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        
        services.AddDomainEventHandlers();
        
        services.AddIntegrationEventHandlers();

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);
    }
    
    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPermissionService, PermissionService>();
        
        services.Configure<KeyCloakOptions>(configuration.GetSection(KeyCloakOptions.ConfigurationSection));

        services.AddTransient<KeyCloakAuthDelegatingHandler>();

        services.AddHttpClient<KeyCloakClient>((serviceProvider, httpClient) =>
        {
            KeyCloakOptions keyCloakOptions = serviceProvider
                .GetRequiredService<IOptions<KeyCloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);
        })
        .AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>()
        .AddStandardResilienceHandler();
        
        services.AddHttpClient<JwtProvider>((serviceProvider, httpClient) =>
        {
            KeyCloakOptions keycloakOptions = 
                serviceProvider.GetRequiredService<IOptions<KeyCloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
        })
        .AddStandardResilienceHandler();

        services.AddTransient<IIdentityProviderService, IdentityProviderService>();
        
        services.AddDbContext<UsersDbContext>((sp, options) => 
            options.UseNpgsql(configuration.GetConnectionStringOrThrow("Database"),
                npgsqlOptions => npgsqlOptions
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.Configure<OutboxOptions>(configuration.GetSection(OutboxOptions.ConfigurationSection));
        
        services.Configure<InboxOptions>(configuration.GetSection(InboxOptions.ConfigurationSection));
        
        services.AddScoped<IOutboxProcessor, OutboxProcessor>();
        services.AddScoped<IInboxProcessor, InboxProcessor>();
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
