using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed record RiskAttitude
{
    private RiskAttitude()
    {
    }
    
    public const decimal MinCoefficient = 1;
    public const decimal MaxCoefficient = 5;
    
    public decimal Coefficient { get; private init; }

    public AttitudeType Type => AttitudeType.FromCoefficient(Coefficient);
    
    internal static Result<RiskAttitude> Determine(decimal coefficient)
    {
        if (coefficient is < MinCoefficient or > MaxCoefficient)
        {
            return Result.Failure<RiskAttitude>(RiskAttitudeErrors.CoefficientOutOfRange);
        }
        
        return new RiskAttitude
        {
            Coefficient = coefficient
        };
    }

    public static RiskAttitude Unspecified => new() { Coefficient = 0 };
}
