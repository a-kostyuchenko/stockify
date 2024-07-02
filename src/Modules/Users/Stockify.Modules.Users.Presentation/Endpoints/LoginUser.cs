using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Users.Application.Authentication;
using Stockify.Modules.Users.Application.Users.Commands.Login;

namespace Stockify.Modules.Users.Presentation.Endpoints;

internal sealed class LoginUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/login", async (Request request, ISender sender) =>
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<AccessToken> result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }

    internal sealed record Request
    {
        public string Email { get; init; }
        
        public string Password { get; init; }
    }
}
