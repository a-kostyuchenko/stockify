using Stockify.Modules.Stocks.Domain.Stockholders;

namespace Stockify.Modules.Stocks.Domain.Portfolios;

public sealed class InvestmentStrategy
{
    private InvestmentStrategy(string name, decimal minRisk, decimal maxRisk, decimal targetReturn)
    {
        Name = name;
        MinRisk = minRisk;
        MaxRisk = maxRisk;
        TargetReturn = targetReturn;
    }

    public string Name { get; }
    public decimal MinRisk { get; }
    public decimal MaxRisk { get; }
    public decimal TargetReturn { get; }
    
    public static readonly InvestmentStrategy Conservative = new(
        nameof(Conservative),
        minRisk: 0.02m,
        maxRisk: 0.08m,
        targetReturn: 0.06m);

    public static readonly InvestmentStrategy Balanced = new(
        nameof(Balanced),
        minRisk: 0.08m,
        maxRisk: 0.15m,
        targetReturn: 0.09m);

    public static readonly InvestmentStrategy Aggressive = new(
        nameof(Aggressive),
        minRisk: 0.15m,
        maxRisk: 0.25m,
        targetReturn: 0.12m);
    
    public static InvestmentStrategy FromRiskProfile(RiskProfile riskProfile)
    {
        return riskProfile.Coefficient switch
        {
            <= 2.0m => Conservative,
            <= 3.5m => Balanced,
            _ => Aggressive
        };
    }
}