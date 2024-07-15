using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Questions.Queries.Get;

namespace Stockify.Modules.Risks.Presentation.Questions;

internal sealed class GetQuestions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Questions.GetQuestions, async (
            ISender sender,
            string? searchTerm,
            int page = 1,
            int pageSize = 15) =>
        {
            var query = new GetQuestionsQuery(searchTerm, page, pageSize);
            
            Result<GetQuestionsResponse> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetQuestions)
        .WithTags(Tags.Questions);
    }
}
