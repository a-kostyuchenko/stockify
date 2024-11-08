using Stockify.Common.Application.Messaging;
using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Application.Users.Commands.Update;

public sealed record UpdateUserCommand(UserId UserId, string FirstName, string LastName) : ICommand;
