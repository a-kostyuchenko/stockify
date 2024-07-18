using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed record RiskAttitude
{
    private RiskAttitude()
    {
    }
    
    public const decimal MinCoefficient = 1;
    public const decimal MaxCoefficient = 5;
    
    public decimal Coefficient { get; init; }

    public RiskAttitudeType Type => RiskAttitudeType.FromCoefficient(Coefficient);
    
    public static Result<RiskAttitude> Estimate(decimal coefficient)
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
    
    // Provide formula for calculating coefficient
    public static decimal CalculateCoefficient(int totalPoints, int maxPoints) => 
        (decimal)totalPoints / maxPoints * 5;

    public static RiskAttitude Unspecified => new() { Coefficient = 0 };
}
