using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public record struct IndividualId(Guid Value) : IEntityId<IndividualId>
{
    public static IndividualId Empty => 
        new(Guid.Empty);

    public static IndividualId New() => 
        new(Guid.NewGuid());

    public static IndividualId FromValue(Guid value) => 
        new(value);
}
