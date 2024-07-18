using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Stockify.Common.Domain;
using Stockify.Common.Presentation.Endpoints;
using Stockify.Common.Presentation.Results;
using Stockify.Modules.Risks.Application.Sessions.Commands.SubmitAnswer;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Presentation.Sessions;

internal sealed class SubmitAnswer : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(Routes.Sessions.SubmitAnswer, async (Guid sessionId, Request request, ISender sender) =>
        {
            var command = new SubmitAnswerCommand(
                SessionId.From(sessionId),
                QuestionId.From(request.QuestionId),
                AnswerId.From(request.AnswerId));

            Result result = await sender.Send(command);

            return result.Match(Results.Created, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifySessions)
        .WithTags(Tags.Sessions);
    }

    internal sealed record Request
    {
        public Guid QuestionId { get; init; }
        public Guid AnswerId { get; init; }
    }
}
