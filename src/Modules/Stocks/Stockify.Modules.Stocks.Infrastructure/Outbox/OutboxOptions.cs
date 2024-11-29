namespace Stockify.Modules.Stocks.Infrastructure.Outbox;

internal sealed class OutboxOptions
{
    public const string ConfigurationSection = "Stocks:Outbox";
    
    public string Schedule { get; init; }
    public int BatchSize { get; set; }
    public int MaxRetries { get; set; }
}
