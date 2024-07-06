namespace Stockify.Modules.Users.Infrastructure.Inbox;

internal sealed class InboxOptions
{
    public const string ConfigurationSection = "Users:Inbox";
    
    public string Schedule { get; init; }
    public int BatchSize { get; set; }
}
