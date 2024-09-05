using MediatR;
using Stockify.Common.Application.Authorization;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Users.Queries.GetPermissions;

namespace Stockify.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService, IScoped
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId) => 
        await sender.Send(new GetUserPermissionsQuery(identityId));
}
