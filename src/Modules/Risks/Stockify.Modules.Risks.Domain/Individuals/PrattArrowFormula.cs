using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class PrattArrowFormula : IFormula
{
    public string Name => "Pratt-Arrow RRA";
    public string Description => "Calculates risk aversion using Pratt-Arrow Relative Risk Aversion measure";

    public Result<decimal> Calculate(SessionScores scores)
    {
        decimal riskTolerance = scores.GetCategoryCoefficient(QuestionCategory.RiskTolerance);
        decimal lossTolerance = scores.GetCategoryCoefficient(QuestionCategory.LossTolerance);
        decimal knowledge = scores.GetCategoryCoefficient(QuestionCategory.FinancialKnowledge);
        
        decimal baseRiskAversion = 1 - riskTolerance;
        
        decimal lossAversionComponent = (1 - lossTolerance) * 0.7m;
        
        decimal knowledgeAdjustment = knowledge * 0.3m;
        
        decimal coefficient = baseRiskAversion + lossAversionComponent - knowledgeAdjustment;
        
        coefficient = 1 + coefficient * 4;
        
        return Math.Round(coefficient, 2);
    }
}
