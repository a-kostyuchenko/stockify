using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Questions;

public record struct QuestionId(Guid Value) : IEntityId<QuestionId>
{
    public static QuestionId Empty => new(Guid.Empty);
    
    public static QuestionId New() => new(Guid.NewGuid());

    public static QuestionId From(Guid value) => new(value);
}
