using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public interface IFormula
{
    Result<decimal> Calculate(int totalPoints, int maxPoints);
}
