using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Sessions.Commands.Complete;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Presentation.Sessions;

internal sealed class CompleteSession : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Sessions.Complete, async (Guid sessionId, ISender sender) =>
        {
            var command = new CompleteSessionCommand(SessionId.From(sessionId));

            Result result = await sender.Send(command);

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifySessions)
        .WithTags(Tags.Sessions);
    }
}
