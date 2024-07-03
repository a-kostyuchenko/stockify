using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Users.Application.Abstractions.Identity;
using Stockify.Modules.Users.Application.Users.Commands.Login;

namespace Stockify.Modules.Users.Presentation.Authentication;

internal sealed class LoginUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("authentication/login", async (Request request, ISender sender) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<TokenResponse> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Authentication);
    }

    internal sealed record Request
    {
        public string Email { get; init; }
        
        public string Password { get; init; }
    }
}
