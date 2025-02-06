namespace Stockify.Common.Domain;

public interface IEntityId<out TId, TValue> : IEntityId where TId : IEntityId<TId, TValue>
{
    TValue Value { get; }
    static abstract TId Empty { get; }
    static abstract TId New();
    static abstract TId From(TValue value);
}

public interface IEntityId<out T> : IEntityId<T, Guid> where T : IEntityId<T, Guid>, IEntityId<T>, IEntityId;

public interface IEntityId;
