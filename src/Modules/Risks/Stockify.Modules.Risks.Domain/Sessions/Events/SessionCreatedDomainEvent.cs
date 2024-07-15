using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Sessions.Events;

public sealed class SessionCreatedDomainEvent(SessionId sessionId) : DomainEvent
{
    public SessionId SessionId { get; init; } = sessionId;
}
