using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Complete;

public sealed record CompleteSessionCommand(SessionId SessionId) : ICommand;
