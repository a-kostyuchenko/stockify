namespace Stockify.Common.Domain;

public abstract class Entity<TEntityId> : IEntity
    where TEntityId : IEntityId
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected Entity(TEntityId id)
        : this()
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    protected Entity() { }

    public TEntityId Id { get; protected init; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
