namespace Stockify.Common.Domain;

public interface IEntityId<out T>
{
    Guid Value { get; }
    static abstract T Empty { get; }
    static abstract T New();
    static abstract T From(Guid value);
}
