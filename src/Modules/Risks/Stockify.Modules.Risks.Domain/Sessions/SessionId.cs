using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Sessions;

public record struct SessionId(Guid Value) : IEntityId<SessionId>
{
    public static SessionId Empty => new(Guid.Empty);
    public static SessionId New() => new(Guid.NewGuid());
    public static SessionId From(Guid value) => new(value);
}
