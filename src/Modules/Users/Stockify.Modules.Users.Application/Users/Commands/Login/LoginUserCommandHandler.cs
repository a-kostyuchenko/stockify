using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Authentication;
using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Application.Users.Commands.Login;

internal sealed class LoginUserCommandHandler(IJwtProvider jwtProvider) 
    : ICommandHandler<LoginUserCommand, AccessToken>
{
    public async Task<Result<AccessToken>> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        Result<string> result = await jwtProvider.GetAccessTokenAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<AccessToken>(UserErrors.InvalidCredentials);
        }

        return new AccessToken(result.Value);
    }
}
