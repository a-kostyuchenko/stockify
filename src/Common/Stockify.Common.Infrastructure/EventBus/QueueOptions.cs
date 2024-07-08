namespace Stockify.Common.Infrastructure.EventBus;

public sealed class QueueOptions
{
    public const string ConfigurationSection = "Queue";
    public string Username { get; init; }
    public string Password { get; init; }
}
