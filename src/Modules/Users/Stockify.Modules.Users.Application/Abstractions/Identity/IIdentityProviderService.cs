using Stockify.Common.Domain;

namespace Stockify.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<Result<string>> RegisterUserAsync(
        UserModel user,
        CancellationToken cancellationToken = default);
    
    Task<Result<TokenResponse>> GetAccessTokensAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
    
    Task<Result<TokenResponse>> RefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken = default);
}
