using System.Reflection;
using NetArchTest.Rules;
using Stockify.ArchitectureTests.Abstractions;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Users.Domain.Users;
using Xunit;

namespace Stockify.ArchitectureTests.Layers;

public class ModuleTests : BaseTest
{
    [Fact]
    public void UsersModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [RisksNamespace, StocksNamespace];
        string[] integrationEventsModules = [
            RisksIntegrationEventsNamespace,
            StocksIntegrationEventsNamespace];

        List<Assembly> usersAssemblies =
        [
            typeof(User).Assembly,
            Modules.Users.Application.AssemblyReference.Assembly,
            Modules.Users.Presentation.AssemblyReference.Assembly,
            Modules.Users.Infrastructure.AssemblyReference.Assembly,
        ];

        Types.InAssemblies(usersAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void RisksModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace, StocksNamespace];
        string[] integrationEventsModules = [
            UsersIntegrationEventsNamespace,
            StocksIntegrationEventsNamespace];

        List<Assembly> risksAssemblies =
        [
            typeof(Individual).Assembly,
            Modules.Risks.Application.AssemblyReference.Assembly,
            Modules.Risks.Presentation.AssemblyReference.Assembly,
            Modules.Risks.Infrastructure.AssemblyReference.Assembly,
        ];

        Types.InAssemblies(risksAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }

    [Fact]
    public void StocksModule_ShouldNotHaveDependencyOn_AnyOtherModule()
    {
        string[] otherModules = [UsersNamespace, RisksNamespace];
        string[] integrationEventsModules = [
            UsersIntegrationEventsNamespace,
            RisksIntegrationEventsNamespace];

        List<Assembly> stocksAssemblies =
        [
            // typeof(Stock).Assembly,
            Modules.Stocks.Application.AssemblyReference.Assembly,
            Modules.Stocks.Presentation.AssemblyReference.Assembly,
            Modules.Stocks.Infrastructure.AssemblyReference.Assembly,
        ];

        Types.InAssemblies(stocksAssemblies)
            .That()
            .DoNotHaveDependencyOnAny(integrationEventsModules)
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .ShouldBeSuccessful();
    }
}
