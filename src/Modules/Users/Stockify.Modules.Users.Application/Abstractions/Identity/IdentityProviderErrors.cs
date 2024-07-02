using Stockify.Common.Domain;

namespace Stockify.Modules.Users.Application.Abstractions.Identity;

public static class IdentityProviderErrors
{
    public static readonly Error EmailIsNotUnique = Error.Conflict(
        "Identity.EmailIsNotUnique",
        "The specified email is not unique.");
    
    public static readonly Error AuthenticationFailed = Error.Problem(
        "Identity.AuthenticationFailed",
        "Failed to acquire access token do to authentication failure");
}
