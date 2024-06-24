using Stockify.Common.Domain;

namespace Stockify.Modules.Users.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "User.NotFound",
        "The user with specified identifier was not found");
}
