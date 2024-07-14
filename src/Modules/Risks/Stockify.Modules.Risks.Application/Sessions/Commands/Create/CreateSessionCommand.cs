using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Individuals;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Create;

public sealed record CreateSessionCommand(IndividualId IndividualId, int QuestionsCount) : ICommand<Guid>;
