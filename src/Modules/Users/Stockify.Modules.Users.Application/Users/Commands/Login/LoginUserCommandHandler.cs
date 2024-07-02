using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Abstractions.Identity;
using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Application.Users.Commands.Login;

internal sealed class LoginUserCommandHandler(IIdentityProviderService identityProviderService) 
    : ICommandHandler<LoginUserCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        Result<TokenResponse> result = await identityProviderService.GetAccessTokensAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<TokenResponse>(UserErrors.InvalidCredentials);
        }

        return result.Value;
    }
}
