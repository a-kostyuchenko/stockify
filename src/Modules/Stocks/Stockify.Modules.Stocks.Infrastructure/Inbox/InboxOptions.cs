namespace Stockify.Modules.Stocks.Infrastructure.Inbox;

internal sealed class InboxOptions
{
    public const string ConfigurationSection = "Stocks:Inbox";
    
    public string Schedule { get; init; }
    public int BatchSize { get; set; }
    public int MaxRetries { get; set; }
}
