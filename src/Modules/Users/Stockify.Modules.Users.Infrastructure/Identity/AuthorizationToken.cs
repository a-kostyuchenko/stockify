using System.Text.Json.Serialization;

namespace Stockify.Modules.Users.Infrastructure.Identity;

public sealed record AuthorizationToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }
}
