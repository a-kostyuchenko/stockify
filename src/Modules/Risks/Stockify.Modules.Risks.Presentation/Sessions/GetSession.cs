using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Sessions.Queries.GetById;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Presentation.Sessions;

internal sealed class GetSession : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Sessions.GetById, async (
            ISender sender,
            Guid sessionId) =>
        {
            var query = new GetSessionByIdQuery(SessionId.FromValue(sessionId));

            Result<SessionResponse> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetSessions)
        .WithName(nameof(GetSession))
        .WithTags(Tags.Sessions);
    }
}
