using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Application.Questions.Commands.AddAnswer;

public sealed record AddAnswerCommand(QuestionId QuestionId, string Content, int Points, string Explanation) : ICommand;
