using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Commands.Start;

public sealed record StartSessionCommand(SessionId SessionId) : ICommand;
