using Stockify.Common.Domain;

namespace Stockify.Modules.Users.Domain.Users.Events;

public sealed class UserUpdatedDomainEvent(UserId userId) : DomainEvent
{
    public UserId UserId { get; init; } = userId;
}
