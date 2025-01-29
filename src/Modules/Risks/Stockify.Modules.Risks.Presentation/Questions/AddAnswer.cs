using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Filters.Idempotency;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Questions.Commands.AddAnswer;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Presentation.Questions;

internal sealed class AddAnswer : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(Routes.Questions.AddAnswer, async (
            Guid questionId,
            Request request,
            ISender sender) =>
        {
            var command = new AddAnswerCommand(
                QuestionId.From(questionId),
                request.Content,
                request.Points,
                request.Explanation);

            Result result = await sender.Send(command);

            return result.Match(Results.Created, ApiResults.Problem);
        })
        .WithTags(Tags.Questions)
        .RequireAuthorization(Permissions.ModifyQuestions)
        .AddEndpointFilter<IdempotencyFilter>();
    }
    
    internal sealed record Request
    {
        public string Content { get; init; }
        public int Points { get; init; }
        public string Explanation { get; init; }
    }
}
