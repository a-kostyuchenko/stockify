namespace Stockify.Modules.Stocks.Infrastructure.Stocks;

internal sealed class AlphavantageOptions
{
    public const string ConfigurationSection = "Stocks:Alphavantage";
    
    public string BaseUrl { get; init; }
    public string ApiKey { get; init; }
}
