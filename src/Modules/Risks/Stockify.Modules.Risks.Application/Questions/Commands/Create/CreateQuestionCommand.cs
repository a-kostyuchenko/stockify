using Stockify.Common.Application.Messaging;

namespace Stockify.Modules.Risks.Application.Questions.Commands.Create;

public sealed record CreateQuestionCommand(string Content, string Category, int Weight) : ICommand<Guid>;
