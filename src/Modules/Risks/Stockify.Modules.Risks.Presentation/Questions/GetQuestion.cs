using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Questions.Queries.GetById;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Presentation.Questions;

internal sealed class GetQuestion : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Questions.GetQuestion, async (Guid questionId, ISender sender) =>
        {
            var query = new GetQuestionByIdQuery(QuestionId.FromValue(questionId));

            Result<QuestionResponse> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Questions)
        .WithName(nameof(GetQuestion))
        .RequireAuthorization(Permissions.GetQuestions);
    }
}
