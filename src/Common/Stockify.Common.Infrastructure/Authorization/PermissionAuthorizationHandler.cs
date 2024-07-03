using Microsoft.AspNetCore.Authorization;
using Stockify.Common.Application.Authorization;
using Stockify.Common.Infrastructure.Authentication;

namespace Stockify.Common.Infrastructure.Authorization;

internal sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        HashSet<string> permissions = context.User.GetPermissions();

        if (permissions.Any(p => p == requirement.Permission ||
                                 p == IPermissionService.AccessEverything))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
