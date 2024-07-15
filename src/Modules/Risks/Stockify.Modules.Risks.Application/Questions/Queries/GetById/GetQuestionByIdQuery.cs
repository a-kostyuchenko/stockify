using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Application.Questions.Queries.GetById;

public sealed record GetQuestionByIdQuery(QuestionId QuestionId) : IQuery<QuestionResponse>;
