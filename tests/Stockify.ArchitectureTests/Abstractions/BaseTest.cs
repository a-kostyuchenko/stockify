namespace Stockify.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected const string UsersNamespace = "Stockify.Modules.Users";
    protected const string UsersIntegrationEventsNamespace = "Stockify.Modules.Users.IntegrationEvents";

    protected const string RisksNamespace = "Stockify.Modules.Risks";
    protected const string RisksIntegrationEventsNamespace = "Stockify.Modules.Risks.IntegrationEvents";

    protected const string StocksNamespace = "Stockify.Modules.Stocks";
    protected const string StocksIntegrationEventsNamespace = "Stockify.Modules.Stocks.IntegrationEvents";
}
