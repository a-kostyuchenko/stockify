using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Users.Application.Users.Queries.GetById;
using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Presentation.Users;

internal sealed class GetUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId:guid}", async (Guid userId, ISender sender) =>
        {
            var query = new GetUserByIdQuery(UserId.FromValue(userId));

            Result<UserResponse> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
