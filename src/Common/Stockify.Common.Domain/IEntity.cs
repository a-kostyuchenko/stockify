namespace Stockify.Common.Domain;

public interface IEntity
{
    void ClearDomainEvents();
    void Raise(IDomainEvent domainEvent);
}
