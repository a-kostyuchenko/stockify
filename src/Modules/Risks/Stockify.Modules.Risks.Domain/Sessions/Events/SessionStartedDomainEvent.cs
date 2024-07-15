using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Sessions.Events;

public sealed class SessionStartedDomainEvent(SessionId sessionId) : DomainEvent
{
    public SessionId SessionId { get; init; } = sessionId;
}
