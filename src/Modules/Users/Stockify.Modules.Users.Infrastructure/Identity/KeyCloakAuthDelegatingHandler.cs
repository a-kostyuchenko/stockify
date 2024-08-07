using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Stockify.Modules.Users.Infrastructure.Authentication;

namespace Stockify.Modules.Users.Infrastructure.Identity;

internal sealed class KeyCloakAuthDelegatingHandler(IOptions<KeyCloakOptions> options) 
    : DelegatingHandler
{
    private readonly KeyCloakOptions _options = options.Value;
    
    private const string GrantType = "client_credentials";
    private const string Scope = "openid";

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        AccessTokens token = await GetAuthorizationToken(cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        
        HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        return httpResponseMessage;
    }

    private async Task<AccessTokens> GetAuthorizationToken(CancellationToken cancellationToken)
    {
        var authRequestParameters = new KeyValuePair<string, string>[]
        {
            new("client_id", _options.ConfidentialClientId),
            new("client_secret", _options.ConfidentialClientSecret),
            new("scope", Scope),
            new("grant_type", GrantType)
        };
        
        using var authRequestContent = new FormUrlEncodedContent(authRequestParameters);

        using var authRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(_options.TokenUrl));

        authRequest.Content = authRequestContent;

        using HttpResponseMessage authorizationResponse = await base.SendAsync(
            authRequest,
            cancellationToken);

        authorizationResponse.EnsureSuccessStatusCode();

        return await authorizationResponse.Content.ReadFromJsonAsync<AccessTokens>(cancellationToken);
    }
}

