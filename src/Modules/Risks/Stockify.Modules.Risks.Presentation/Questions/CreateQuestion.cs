using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Questions.Commands.Create;

namespace Stockify.Modules.Risks.Presentation.Questions;

internal sealed class CreateQuestion : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Questions.Create, async (Request request, ISender sender) =>
        {
            var command = new CreateQuestionCommand(request.Content);

            Result<Guid> result = await sender.Send(command);

            return result.Match(
                questionId => Results.CreatedAtRoute(nameof(GetQuestion), new { questionId }, questionId),
                ApiResults.Problem);
        })
        .WithTags(Tags.Questions)
        .RequireAuthorization(Permissions.ModifyQuestions);
    }
    
    internal sealed record Request
    {
        public string Content { get; init; }
    }
}
