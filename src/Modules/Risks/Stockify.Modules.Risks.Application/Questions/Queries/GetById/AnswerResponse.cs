namespace Stockify.Modules.Risks.Application.Questions.Queries.GetById;

public sealed record AnswerResponse(Guid AnswerId, string Content, int Points);
