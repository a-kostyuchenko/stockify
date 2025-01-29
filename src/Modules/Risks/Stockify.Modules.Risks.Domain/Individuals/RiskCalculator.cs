using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class RiskCalculator : ICalculator
{
    public Result<RiskAttitude> Calculate(Session session, IFormula formula)
    {
        Result<decimal> result = formula.Calculate(session.TotalPoints, session.MaxPoints);

        if (result.IsFailure)
        {
            return Result.Failure<RiskAttitude>(result.Error);
        }

        return RiskAttitude.Determine(result.Value);
    }
}