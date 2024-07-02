using Stockify.Common.Application.Messaging;
using Stockify.Modules.Users.Application.Authentication;

namespace Stockify.Modules.Users.Application.Users.Commands.Login;

public sealed record LoginUserCommand(string Email, string Password)
    : ICommand<AccessToken>;
