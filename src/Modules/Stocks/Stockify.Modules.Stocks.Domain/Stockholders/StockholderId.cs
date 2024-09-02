using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Stockholders;

public record struct StockholderId(Guid Value) : IEntityId<StockholderId>
{
    public static StockholderId Empty => new(Guid.Empty);
    
    public static StockholderId New() => new(Guid.NewGuid());

    public static StockholderId From(Guid value) => new(value);
}
