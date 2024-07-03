using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Stockify.Common.Application.Authorization;
using Stockify.Common.Infrastructure.Configuration;
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
        
        services.AddDbContext<UsersDbContext>((_, options) => 
            options.UseNpgsql(configuration.GetConnectionStringOrThrow("Database"),
                npgsqlOptions => npgsqlOptions
                    .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());
    }
}
