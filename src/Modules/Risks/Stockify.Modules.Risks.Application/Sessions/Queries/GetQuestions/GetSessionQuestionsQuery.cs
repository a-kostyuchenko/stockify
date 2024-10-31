using Stockify.Common.Application.Messaging;
using Stockify.Common.Application.Pagination;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetQuestions;

public sealed record GetSessionQuestionsQuery(
    SessionId SessionId,
    IndividualId IndividualId,
    int Page,
    int PageSize
) : IQuery<PagedResponse<QuestionResponse>>;
