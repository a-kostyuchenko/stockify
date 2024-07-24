namespace Stockify.Modules.Users.Infrastructure.Identity;

internal sealed class KeyCloakOptions
{
    public const string ConfigurationSection = "Users:KeyCloak";
    
    public string AdminUrl { get; set; }

    public string TokenUrl { get; set; }

    public string ConfidentialClientId { get; set; }

    public string ConfidentialClientSecret { get; set; }

    public string PublicClientId { get; set; }
}
