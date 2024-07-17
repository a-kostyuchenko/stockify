using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Infrastructure.Authentication;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Users.Application.Users.Queries.GetById;
using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Users.GetProfile, async (ISender sender, ClaimsPrincipal claimsPrincipal) =>
        {
            var query = new GetUserByIdQuery(UserId.From(claimsPrincipal.GetUserId()));

            Result<UserResponse> result = await sender.Send(query);
            
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.AccessUsers)
        .WithTags(Tags.Users);
    }
}
