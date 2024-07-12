namespace Stockify.Modules.Risks.Infrastructure.Outbox;

internal sealed class OutboxOptions
{
    public const string ConfigurationSection = "Risks:Outbox";
    
    public string Schedule { get; init; }
    public int BatchSize { get; set; }
}
