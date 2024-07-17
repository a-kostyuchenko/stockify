using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Questions;

public record struct AnswerId(Guid Value) : IEntityId<AnswerId>
{
    public static AnswerId Empty => new(Guid.Empty);
    
    public static AnswerId New() => new(Guid.NewGuid());

    public static AnswerId From(Guid value) => new(value);
}
