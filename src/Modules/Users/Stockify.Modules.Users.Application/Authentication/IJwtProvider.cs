using Stockify.Common.Domain;

namespace Stockify.Modules.Users.Application.Authentication;

public interface IJwtProvider
{
    Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}
