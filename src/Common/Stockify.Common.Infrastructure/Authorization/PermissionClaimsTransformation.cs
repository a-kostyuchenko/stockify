using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Stockify.Common.Application.Authorization;
using Stockify.Common.Application.Exceptions;
using Stockify.Common.Domain;
using Stockify.Common.Infrastructure.Authentication;

namespace Stockify.Common.Infrastructure.Authorization;

internal sealed class PermissionClaimsTransformation(IServiceScopeFactory serviceScopeFactory) 
    : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.HasClaim(c => c.Type == JwtRegisteredClaimNames.Sub))
        {
            return principal;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();

        IPermissionService permissionService = scope.ServiceProvider
            .GetRequiredService<IPermissionService>();

        string identityId = principal.GetIdentityId();

        Result<PermissionsResponse> result = await permissionService.GetUserPermissionsAsync(identityId);
        
        if (result.IsFailure)
        {
            throw new StockifyException(
                nameof(IPermissionService.GetUserPermissionsAsync),
                result.Error);
        }
        
        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, result.Value.UserId.ToString()));
        
        foreach (string permission in result.Value.Permissions)
        {
            claimsIdentity.AddClaim(new Claim(CustomClaims.Permission, permission));
        }

        principal.AddIdentity(claimsIdentity);

        return principal;
    }
}
