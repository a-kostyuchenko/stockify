using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Application.Abstractions.Authentication;

public interface IUserContext
{
    UserId UserId { get; }
}
