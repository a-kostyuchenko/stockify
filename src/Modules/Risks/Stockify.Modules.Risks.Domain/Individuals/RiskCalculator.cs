using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class RiskCalculator(IFormulaSelector formulaSelector) : ICalculator
{
    public Result<CalculationResult> Calculate(Session session)
    {
        var scores = new SessionScores(session.GetScoresByCategory());
        IReadOnlySet<QuestionCategory> availableCategories = scores.GetAvailableCategories();

        Result<IFormula> formulaResult = formulaSelector.Select(availableCategories);

        if (formulaResult.IsFailure)
        {
            return Result.Failure<CalculationResult>(formulaResult.Error);
        }
        
        IFormula formula = formulaResult.Value;
        
        // Log usage of basic formula as warning
        
        Result<decimal> result = formula.Calculate(scores);

        if (result.IsFailure)
        {
            return Result.Failure<CalculationResult>(result.Error);
        }

        Result<RiskAttitude> attitudeResult = RiskAttitude.Determine(result.Value);

        if (attitudeResult.IsFailure)
        {
            return Result.Failure<CalculationResult>(attitudeResult.Error);
        }

        return new CalculationResult(
            attitudeResult.Value,
            formula.Type,
            [.. availableCategories]);
    }
}
