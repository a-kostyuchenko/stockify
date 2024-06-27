namespace Stockify.Modules.Users.Application.Users.Queries.GetById;

public sealed record UserResponse(Guid Id, string Email, string FirstName, string LastName);
