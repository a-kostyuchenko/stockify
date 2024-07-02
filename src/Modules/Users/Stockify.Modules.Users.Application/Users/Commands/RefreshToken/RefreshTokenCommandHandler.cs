using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Abstractions.Identity;

namespace Stockify.Modules.Users.Application.Users.Commands.RefreshToken;

internal sealed class RefreshTokenCommandHandler(IIdentityProviderService identityProviderService)
    : ICommandHandler<RefreshTokenCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        Result<TokenResponse> result = await identityProviderService.RefreshTokenAsync(
            request.RefreshToken,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<TokenResponse>(result.Error);
        }

        return result.Value;
    }
}
