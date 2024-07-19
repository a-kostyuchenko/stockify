using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Abstractions.Authentication;
using Stockify.Modules.Risks.Application.Sessions.Queries.GetResult;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Presentation.Sessions;

internal sealed class GetResult : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Sessions.GetResult, async (
            Guid sessionId,
            IIndividualContext individualContext,
            ISender sender) =>
        {
            var query = new GetSessionResultQuery(SessionId.From(sessionId), individualContext.IndividualId);

            Result<SessionResultResponse> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Sessions)
        .RequireAuthorization(Permissions.GetSessions);
    }
}
