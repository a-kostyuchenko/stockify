namespace Stockify.Modules.Stocks.Domain.Stockholders;

public sealed record RiskProfile
{
    private RiskProfile()
    {
    }

    public decimal Coefficient { get; private set; }
    public string AttitudeType { get; private set; } = string.Empty;
    public DateTime? UpdatedAtUtc { get; private set; }
    
    public static RiskProfile Create(decimal coefficient, string attitudeType) => 
        new()
        {
            Coefficient = coefficient,
            AttitudeType = attitudeType,
            UpdatedAtUtc = DateTime.UtcNow
        };
    
    public static readonly RiskProfile Empty = new();
}
