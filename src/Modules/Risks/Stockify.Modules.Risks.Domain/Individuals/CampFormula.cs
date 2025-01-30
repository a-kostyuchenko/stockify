using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class CampFormula : IFormula
{
    public string Name => "Capital Asset Pricing Model";
    public string Description => "Uses Capital Asset Pricing Model principles for risk assessment";
    public Result<decimal> Calculate(SessionScores scores)
    {
        decimal riskTolerance = scores.GetCategoryCoefficient(QuestionCategory.RiskTolerance);
        decimal knowledge = scores.GetCategoryCoefficient(QuestionCategory.FinancialKnowledge);
        decimal experience = scores.GetCategoryCoefficient(QuestionCategory.InvestmentExperience);

        // Market risk premium (typically 4-8%)
        const decimal marketRiskPremium = 0.06m;
        
        // Risk-free rate (typically government bond yield)
        const decimal riskFreeRate = 0.02m;
        
        decimal beta = 0.5m + riskTolerance * 1.5m; // Beta range: 0.5 - 2
        
        decimal adjustedBeta = beta * (1 + (knowledge + experience) / 4);
        
        decimal requiredReturn = riskFreeRate + adjustedBeta * marketRiskPremium;
        
        decimal coefficient = 1 + (requiredReturn - 0.02m) / 0.08m * 4;
        
        return Math.Min(5, Math.Max(1, Math.Round(coefficient, 2)));
    }
}