namespace Stockify.Common.Domain;

public interface IEntityId<out T>
{
    Guid Value { get; }
    static T Empty { get; }
    static abstract T New();
    static abstract T FromValue(Guid value);
}
