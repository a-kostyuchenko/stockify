namespace Stockify.Modules.Risks.Infrastructure.Inbox;

internal sealed class InboxOptions
{
    public const string ConfigurationSection = "Risks:Inbox";
    
    public string Schedule { get; init; }
    public int BatchSize { get; set; }
    public int MaxRetries { get; set; }
}
