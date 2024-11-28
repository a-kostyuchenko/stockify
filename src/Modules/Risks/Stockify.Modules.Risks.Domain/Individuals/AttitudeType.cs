using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class AttitudeType : Enumeration<AttitudeType>
{
    private static readonly AttitudeType Unspecified = new(0, "unspecified");
    
    public static readonly AttitudeType Averse = new(1, "averse");
    public static readonly AttitudeType Neutral = new(2, "neutral");
    public static readonly AttitudeType Seeking = new(3, "seeking");

    private AttitudeType(int id, string name) 
        : base(id, name)
    {
    }
    
    public static AttitudeType FromCoefficient(decimal coefficient)
    {
        return coefficient switch
        {
            1 => Averse,
            2 => Seeking,
            3 => Neutral,
            _ => Unspecified
        };
    }
}
