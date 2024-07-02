using Stockify.Common.Application.Messaging;
using Stockify.Modules.Users.Application.Abstractions.Identity;

namespace Stockify.Modules.Users.Application.Users.Commands.Login;

public sealed record LoginUserCommand(string Email, string Password)
    : ICommand<TokenResponse>;
