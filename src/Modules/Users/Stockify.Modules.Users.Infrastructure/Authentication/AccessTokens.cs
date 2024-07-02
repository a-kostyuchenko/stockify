using System.Text.Json.Serialization;
using Stockify.Modules.Users.Application.Abstractions.Identity;

namespace Stockify.Modules.Users.Infrastructure.Authentication;

public sealed record AccessTokens
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; }
    
    public TokenResponse CreateTokenResponse() => 
        new(AccessToken, RefreshToken);
}
