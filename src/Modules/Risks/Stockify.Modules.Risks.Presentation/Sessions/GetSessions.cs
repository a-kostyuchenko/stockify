using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Application.Pagination;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Abstractions.Authentication;
using Stockify.Modules.Risks.Application.Sessions.Queries.Get;
using Stockify.Modules.Risks.Application.Sessions.Queries.GetById;

namespace Stockify.Modules.Risks.Presentation.Sessions;

internal sealed class GetSessions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                Routes.Sessions.Get,
                async (
                    ISender sender,
                    IIndividualContext individualContext,
                    string? status,
                    DateTime? startedAtUtc,
                    DateTime? completedAtUtc,
                    int page = 1,
                    int pageSize = 15
                ) =>
                {
                    var query = new GetSessionsQuery(
                        individualContext.IndividualId,
                        status,
                        startedAtUtc,
                        completedAtUtc,
                        page,
                        pageSize
                    );

                    Result<PagedResponse<SessionResponse>> result = await sender.Send(query);

                    return result.Match(Results.Ok, ApiResults.Problem);
                }
            )
            .RequireAuthorization(Permissions.GetSessions)
            .WithTags(Tags.Sessions);
    }
}
