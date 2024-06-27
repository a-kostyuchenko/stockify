namespace Stockify.Modules.Users.Infrastructure.Identity;

internal sealed class KeyCloakOptions
{
    public const string ConfigurationSection = "Users:KeyCloak";
    
    public string AdminUrl { get; init; }

    public string TokenUrl { get; init; }

    public string ConfidentialClientId { get; init; }

    public string ConfidentialClientSecret { get; init; }

    public string PublicClientId { get; init; }
}
