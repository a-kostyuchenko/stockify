using Stockify.Common.Domain;

namespace Stockify.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<Result<string>> RegisterUserAsync(
        UserModel user,
        CancellationToken cancellationToken = default);
}
