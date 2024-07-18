using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Abstractions.Authentication;
using Stockify.Modules.Risks.Application.Sessions.Queries.GetQuestions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Presentation.Sessions;

internal sealed class GetSessionQuestions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Sessions.GetQuestions, async (
            ISender sender,
            IIndividualContext individualContext,
            Guid sessionId,
            int page = 1,
            int pageSize = 15) =>
        {
            var query = new GetSessionQuestionsQuery(
                SessionId.From(sessionId),
                individualContext.IndividualId,
                page,
                pageSize);

            Result<GetSessionQuestionsResponse> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetSessionQuestions)
        .WithTags(Tags.Sessions);
    }
}
