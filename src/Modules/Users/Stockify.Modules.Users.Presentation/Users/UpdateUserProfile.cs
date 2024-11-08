using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Users.Application.Abstractions.Authentication;
using Stockify.Modules.Users.Application.Users.Commands.Update;

namespace Stockify.Modules.Users.Presentation.Users;

internal sealed class UpdateUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
                Routes.Users.UpdateProfile,
                async (Request request, ISender sender, IUserContext userContext) =>
                {
                    var command = new UpdateUserCommand(
                        userContext.UserId,
                        request.FirstName,
                        request.LastName
                    );

                    Result result = await sender.Send(command);

                    return result.Match(Results.NoContent, ApiResults.Problem);
                }
            )
            .RequireAuthorization(Permissions.AccessUsers)
            .WithTags(Tags.Users);
    }

    internal sealed record Request
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }
}
