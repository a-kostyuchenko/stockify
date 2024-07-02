using Stockify.Common.Application.Messaging;
using Stockify.Modules.Users.Application.Abstractions.Identity;

namespace Stockify.Modules.Users.Application.Users.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<TokenResponse>;
