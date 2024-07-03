using Stockify.Common.Domain;

namespace Stockify.Common.Application.Authorization;

public interface IPermissionService
{
    public const string AccessEverything = "administrator:full_access";
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}
