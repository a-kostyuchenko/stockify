using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Users.Application.Abstractions.Identity;
using Stockify.Modules.Users.Application.Users.Commands.RefreshToken;

namespace Stockify.Modules.Users.Presentation.Authentication;

internal sealed class RefreshToken : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("authentication/refresh-token", async (Request request, ISender sender) =>
        {
            var command = new RefreshTokenCommand(request.RefreshToken);

            Result<TokenResponse> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Authentication);
    }
    
    internal sealed record Request
    {
        public string RefreshToken { get; init; }
    }
}
