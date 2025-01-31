using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class BlackScholesFormula : IFormula
{
    public string Name => "Black-Scholes Based";
    public string Description => "Uses option pricing theory for risk assessment";
    public FormulaType Type => FormulaType.BlackScholes;

    public Result<decimal> Calculate(SessionScores scores)
    {
        decimal riskTolerance = scores.GetCategoryCoefficient(QuestionCategory.RiskTolerance);
        decimal timeHorizon = scores.GetCategoryCoefficient(QuestionCategory.InvestmentHorizon);
        
        decimal volatility = 0.15m + riskTolerance * 0.25m; // 15-40% volatility range
        
        decimal timeToMaturity = 0.5m + timeHorizon * 4.5m; // 0.5-5 years
        
        // Calculate d1 from Black-Scholes (simplified)
        double d1 = Math.Log(1 / 0.95) // Strike price ratio
                    + (double)(0.02m + volatility * volatility / 2) * (double)timeToMaturity 
                    / (double)(volatility * (decimal)Math.Sqrt((double)timeToMaturity));
        
        decimal normalizedRisk = (decimal)(Math.Abs(d1) / 3.0); // Normalize to typical d1 range
        
        decimal coefficient = 1 + normalizedRisk * 4;
        
        return Math.Min(5, Math.Max(1, Math.Round(coefficient, 2)));
    }
}
