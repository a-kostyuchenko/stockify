using Stockify.Common.Application.Authorization;
using Stockify.Common.Application.Messaging;

namespace Stockify.Modules.Users.Application.Users.Queries.GetPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
