using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class RiskCalculator : ICalculator
{
    public Result<RiskAttitude> Calculate(Session session, IFormula formula)
    {
        IDictionary<QuestionCategory, (int Total, int Max)> categoryScores = session.GetScoresByCategory();

        var scores = new SessionScores(categoryScores);
        
        Result<decimal> result = formula.Calculate(scores);

        if (result.IsFailure)
        {
            return Result.Failure<RiskAttitude>(result.Error);
        }

        return RiskAttitude.Determine(result.Value);
    }
}
