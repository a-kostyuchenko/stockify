using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Abstractions.Identity;
using Stockify.Modules.Users.Infrastructure.Identity;

namespace Stockify.Modules.Users.Infrastructure.Authentication;

internal sealed class JwtProvider(HttpClient httpClient, IOptions<KeyCloakOptions> options)
{
    private readonly KeyCloakOptions _keyCloakOptions = options.Value;
    
    public async Task<Result<AccessTokens>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var authRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _keyCloakOptions.PublicClientId),
            new("scope", "openid email"),
            new("grant_type", "password"),
            new("username", email),
            new("password", password)
        };

        using var authorizationRequestContent = new FormUrlEncodedContent(authRequestParameters);

        HttpResponseMessage response = await httpClient.PostAsync(
            string.Empty,
            authorizationRequestContent,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        AccessTokens? tokens = await response
            .Content
            .ReadFromJsonAsync<AccessTokens>(cancellationToken);

        if (tokens is null)
        {
            return Result.Failure<AccessTokens>(IdentityProviderErrors.AuthenticationFailed);
        }

        return tokens;
    }

    public async Task<Result<AccessTokens>> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken)
    {
        var requestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _keyCloakOptions.PublicClientId),
            new("grant_type", "refresh_token"),
            new("refresh_token", refreshToken)
        };
        
        using var requestContent = new FormUrlEncodedContent(requestParameters);

        HttpResponseMessage response = await httpClient.PostAsync(
            string.Empty,
            requestContent,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        AccessTokens? tokens = await response
            .Content
            .ReadFromJsonAsync<AccessTokens>(cancellationToken);

        if (tokens is null)
        {
            return Result.Failure<AccessTokens>(IdentityProviderErrors.TokenRefreshFailed);
        }

        return tokens;
    }
}
