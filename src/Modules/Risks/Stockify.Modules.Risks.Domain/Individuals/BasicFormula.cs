using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class BasicFormula : IFormula
{
    public string Name => "Basic";
    public string Description => "Calculates risk aversion using a basic formula";
    public FormulaType Type => FormulaType.Basic;

    private static readonly Dictionary<QuestionCategory, decimal> CategoryWeights = new()
    {
        { QuestionCategory.RiskTolerance, 1.0m },
        { QuestionCategory.LossTolerance, 0.8m },
        { QuestionCategory.FinancialKnowledge, 0.5m },
        { QuestionCategory.InvestmentExperience, 0.4m },
        { QuestionCategory.InvestmentHorizon, 0.7m },
        { QuestionCategory.IncomeStability, 0.6m },
    };
    
    public Result<decimal> Calculate(SessionScores scores)
    {
        IReadOnlySet<QuestionCategory> availableCategories = scores.GetAvailableCategories();

        if (!availableCategories.Any())
        {
            return Result.Failure<decimal>(FormulaErrors.NoCategories);
        }

        decimal totalScore = decimal.Zero;
        decimal totalWeight = decimal.Zero;
        
        foreach (QuestionCategory category in availableCategories)
        {
            decimal coefficient = scores.GetCategoryCoefficient(category);
            decimal weight = CategoryWeights.GetValueOrDefault(category, 0.5m);
            
            totalScore += coefficient * weight;
            totalWeight += weight;
        }

        if (totalWeight == decimal.Zero)
        {
            return Result.Failure<decimal>(FormulaErrors.NoData);
        }

        decimal normalizedScore = 1 + totalScore / totalWeight * 4;

        return Math.Round(normalizedScore, 2);
    }
}
