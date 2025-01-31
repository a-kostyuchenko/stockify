using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class VaRFormula : IFormula
{
    public string Name => "Value at Risk";
    public string Description => "Calculates risk aversion using Value at Risk principles";
    public FormulaType Type => FormulaType.VaR;

    public Result<decimal> Calculate(SessionScores scores)
    {
        decimal riskTolerance = scores.GetCategoryCoefficient(QuestionCategory.RiskTolerance);
        decimal lossTolerance = scores.GetCategoryCoefficient(QuestionCategory.LossTolerance);
        
        decimal confidenceLevel = 0.95m + riskTolerance * 0.04m;
        
        decimal maxLossTolerance = lossTolerance * 100;
        
        decimal var = maxLossTolerance * (decimal)Math.Sqrt(-2 * Math.Log((double)(1 - confidenceLevel)));
        
        decimal coefficient = 1 + var / 25;
        
        return Math.Min(5, Math.Max(1, Math.Round(coefficient, 2)));
    }
}
