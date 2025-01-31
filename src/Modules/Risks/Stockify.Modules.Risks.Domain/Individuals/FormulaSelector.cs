using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class FormulaSelector : IFormulaSelector
{
    private sealed class FormulaDefinition
    {
        public HashSet<QuestionCategory> RequiredCategories { get; init; }
        public Func<IFormula> Factory { get; init; }
    }

    private readonly Dictionary<FormulaType, FormulaDefinition> _formulas = new()
    {
        {
            FormulaType.VaR,
            new FormulaDefinition
            {
                RequiredCategories = [QuestionCategory.RiskTolerance, QuestionCategory.LossTolerance],
                Factory = () => new VaRFormula()
            }
        },
        {
            FormulaType.Camp, 
            new FormulaDefinition
            {
                RequiredCategories = [
                    QuestionCategory.RiskTolerance,
                    QuestionCategory.FinancialKnowledge,
                    QuestionCategory.InvestmentExperience
                ],
                Factory = () => new CampFormula()
            }
        },
        {
            FormulaType.PrattArrow,
            new FormulaDefinition
            {
                RequiredCategories = [
                    QuestionCategory.RiskTolerance,
                    QuestionCategory.LossTolerance,
                    QuestionCategory.FinancialKnowledge
                ],
                Factory = () => new PrattArrowFormula()
            }
        },
        {
            FormulaType.BlackScholes,
            new FormulaDefinition
            {
                RequiredCategories = [QuestionCategory.RiskTolerance, QuestionCategory.InvestmentHorizon],
                Factory = () => new BlackScholesFormula()
            }
        }
    };

    public Result<IFormula> Select(IReadOnlySet<QuestionCategory> availableCategories)
    {
        var formulaScores = _formulas.Select(f => new
            {
                FormulaType = f.Key,
                Definition = f.Value,
                Score = CalculateScore(f.Value, availableCategories)
            })
            .Where(f => f.Score.IsValid)
            .OrderByDescending(f => f.Score.Value)
            .ToList();

        if (!formulaScores.Any())
        {
            if (!availableCategories.Any())
            {
                return Result.Failure<IFormula>(FormulaErrors.NoCategories);
            }

            return new BasicFormula();
        }
        
        IFormula bestFormula = formulaScores[0].Definition.Factory();

        return Result.Success(bestFormula);
    }

    private static FormulaScore CalculateScore(
        FormulaDefinition definition,
        IReadOnlySet<QuestionCategory> availableCategories)
    {
        if (!definition.RequiredCategories.IsSubsetOf(availableCategories))
        {
            return FormulaScore.Invalid;
        }

        decimal matchCategories = definition.RequiredCategories.Count;
        decimal totalAvailableCategories = availableCategories.Count;

        decimal coverage = matchCategories / totalAvailableCategories * 5;

        return new FormulaScore(coverage);
    }
}
