using Stockify.Common.Application.Messaging;

namespace Stockify.Modules.Risks.Application.Questions.Queries.Get;

public sealed record GetQuestionsQuery(string? SearchTerm, int Page, int PageSize) 
    : IQuery<GetQuestionsResponse>;
