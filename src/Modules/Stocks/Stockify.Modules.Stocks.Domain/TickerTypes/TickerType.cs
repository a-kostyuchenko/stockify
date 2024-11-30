using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.TickerTypes;

public sealed class TickerType : Entity<TickerTypeId>
{
    private TickerType() : base(TickerTypeId.New())
    {
    }
    
    public string Code { get; private set; }
    public string Description { get; private set; }
    
    public static TickerType Create(string code, string description)
    {
        return new TickerType
        {
            Code = code,
            Description = description,
        };
    }
    
    public void Update(string code, string description)
    {
        if (Code == code && Description == description)
        {
            return;
        }
        
        Code = code;
        Description = description;
    }
}
