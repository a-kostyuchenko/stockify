using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Filters.Idempotency;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Users.Application.Users.Commands.Register;

namespace Stockify.Modules.Users.Presentation.Users;

internal sealed class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Users.Register, async (Request request, ISender sender) =>
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.FirstName,
                request.LastName,
                request.Password);

            Result<Guid> result = await sender.Send(command);

            return result.Match(
                userId => Results.CreatedAtRoute(nameof(GetUser), new { userId }, userId),
                ApiResults.Problem);
        })
        .AllowAnonymous()
        .WithTags(Tags.Users)
        .AddEndpointFilter<IdempotencyFilter>();
    }
    
    internal sealed record Request
    {
        public string Email { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }
        
        public string Password { get; init; }
    }
}
