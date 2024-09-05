using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Stockify.Common.Application.ServiceLifetimes;

namespace Stockify.Common.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTransientAsMatchingInterfaces(this IServiceCollection services, params Assembly[] assemblies) =>
        services.Scan(scan =>
            scan.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo<ITransient>(), false)
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsMatchingInterface()
                .WithTransientLifetime());
    
    public static IServiceCollection AddScopedAsMatchingInterfaces(this IServiceCollection services, params Assembly[] assemblies) =>
        services.Scan(scan =>
            scan.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo<IScoped>(), false)
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsMatchingInterface()
                .WithTransientLifetime());
    
    public static IServiceCollection AddSingletonAsMatchingInterfaces(this IServiceCollection services, params Assembly[] assemblies) =>
        services.Scan(scan =>
            scan.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo<ISingleton>(), false)
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsMatchingInterface()
                .WithSingletonLifetime());
}
