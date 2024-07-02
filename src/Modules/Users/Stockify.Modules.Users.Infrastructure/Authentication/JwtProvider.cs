using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Abstractions.Identity;
using Stockify.Modules.Users.Application.Authentication;
using Stockify.Modules.Users.Infrastructure.Identity;

namespace Stockify.Modules.Users.Infrastructure.Authentication;

internal sealed class JwtProvider(
    HttpClient httpClient,
    IOptions<KeyCloakOptions> options) : IJwtProvider
{
    private readonly KeyCloakOptions _keyCloakOptions = options.Value;
    
    public async Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        try
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
                "",
                authorizationRequestContent,
                cancellationToken);

            response.EnsureSuccessStatusCode();

            AuthorizationToken? authorizationToken = await response
                .Content
                .ReadFromJsonAsync<AuthorizationToken>(cancellationToken);

            if (authorizationToken is null)
            {
                return Result.Failure<string>(IdentityProviderErrors.AuthenticationFailed);
            }

            return authorizationToken.AccessToken;
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(IdentityProviderErrors.AuthenticationFailed);
        }
    }
}
