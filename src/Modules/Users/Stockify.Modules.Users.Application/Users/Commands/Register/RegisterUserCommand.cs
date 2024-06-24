using Stockify.Common.Application.Messaging;

namespace Stockify.Modules.Users.Application.Users.Commands.Register;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string Password) : ICommand<Guid>;
