using System.Net;
using Microsoft.Extensions.Logging;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Abstractions.Identity;
using Stockify.Modules.Users.Infrastructure.Authentication;

namespace Stockify.Modules.Users.Infrastructure.Identity;

internal sealed class IdentityProviderService(
    KeyCloakClient keyCloakClient,
    JwtProvider jwtProvider,
    ILogger<IdentityProviderService> logger) : IIdentityProviderService, ITransient
{
    private const string PasswordCredentialType = "password";
    
    public async Task<Result<string>> RegisterUserAsync(
        UserModel user,
        CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]);

        try
        {
            string identityId = await keyCloakClient.RegisterUserAsync(
                userRepresentation,
                cancellationToken);

            return identityId;
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(exception, "User registration failed");

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }

    public async Task<Result<TokenResponse>> GetAccessTokensAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Result<AccessTokens> result = await jwtProvider.GetAccessTokenAsync(
                email,
                password,
                cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure<TokenResponse>(result.Error);
            }

            return result.Value.CreateTokenResponse();

        }
        catch (HttpRequestException)
        {
            return Result.Failure<TokenResponse>(IdentityProviderErrors.AuthenticationFailed);
        }
    }

    public async Task<Result<TokenResponse>> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Result<AccessTokens> result = await jwtProvider.RefreshTokenAsync(
                refreshToken,
                cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure<TokenResponse>(result.Error);
            }

            return result.Value.CreateTokenResponse();

        }
        catch (HttpRequestException)
        {
            return Result.Failure<TokenResponse>(IdentityProviderErrors.TokenRefreshFailed);
        }
    }
}
