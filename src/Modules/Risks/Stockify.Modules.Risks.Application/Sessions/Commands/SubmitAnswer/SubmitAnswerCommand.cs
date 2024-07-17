using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.SubmitAnswer;

public sealed record SubmitAnswerCommand(
    SessionId SessionId,
    QuestionId QuestionId,
    AnswerId AnswerId) : ICommand;
