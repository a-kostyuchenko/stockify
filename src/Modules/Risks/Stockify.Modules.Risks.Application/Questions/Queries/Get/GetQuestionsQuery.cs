using Stockify.Common.Application.Messaging;
using Stockify.Common.Application.Pagination;

namespace Stockify.Modules.Risks.Application.Questions.Queries.Get;

public sealed record GetQuestionsQuery(string? SearchTerm, int Page, int PageSize)
    : IQuery<PagedResponse<QuestionResponse>>;
