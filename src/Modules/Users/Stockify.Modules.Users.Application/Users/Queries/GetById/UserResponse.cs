using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Application.Users.Queries.GetById;

public sealed record UserResponse(UserId Id, string Email, string FirstName, string LastName);
