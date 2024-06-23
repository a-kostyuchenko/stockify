using Stockify.Common.Domain;

namespace Stockify.Modules.Users.Domain.Users;

public readonly record struct UserId(Guid Value) : IEntityId<UserId>
{
    public static UserId Empty => new(Guid.Empty);
    public static UserId New() => new(Guid.NewGuid());
    public static UserId FromValue(Guid value) => new(value);
}
