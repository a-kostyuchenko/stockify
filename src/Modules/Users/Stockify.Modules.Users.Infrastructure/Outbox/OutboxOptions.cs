namespace Stockify.Modules.Users.Infrastructure.Outbox;

internal sealed class OutboxOptions
{
    public const string ConfigurationSection = "Users:Outbox";
    
    public string Schedule { get; init; }
    public int BatchSize { get; set; }
    public int MaxRetries { get; set; }
}
