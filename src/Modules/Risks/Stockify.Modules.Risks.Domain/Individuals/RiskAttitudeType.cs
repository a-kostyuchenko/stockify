using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class RiskAttitudeType : Enumeration<RiskAttitudeType>
{
    private static readonly RiskAttitudeType Unspecified = new(0, "unspecified");
    
    public static readonly RiskAttitudeType Averse = new(1, "averse");
    public static readonly RiskAttitudeType Neutral = new(2, "neutral");
    public static readonly RiskAttitudeType Loving = new(3, "loving");

    private RiskAttitudeType(int id, string name) 
        : base(id, name)
    {
    }
    
    public static RiskAttitudeType FromCoefficient(decimal coefficient)
    {
        return coefficient switch
        {
            1 => Averse,
            2 => Loving,
            3 => Neutral,
            _ => Unspecified
        };
    }
}