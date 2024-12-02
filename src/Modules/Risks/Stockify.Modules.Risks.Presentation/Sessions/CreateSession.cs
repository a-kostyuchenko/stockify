using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Filters.Idempotency;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Abstractions.Authentication;
using Stockify.Modules.Risks.Application.Sessions.Commands.Create;

namespace Stockify.Modules.Risks.Presentation.Sessions;

internal sealed class CreateSession : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Sessions.Create, async (
            Request request,
            IIndividualContext individualContext,
            ISender sender) =>
        {
            var command = new CreateSessionCommand(individualContext.IndividualId, request.QuestionsCount);

            Result<Guid> result = await sender.Send(command);

            return result.Match(
                sessionId => Results.CreatedAtRoute(nameof(GetSession), new { sessionId }, sessionId),
                ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifySessions)
        .WithTags(Tags.Sessions)
        .AddEndpointFilter<IdempotencyFilter>();
    }

    internal sealed record Request
    {
        public int QuestionsCount { get; set; }
    }
}
