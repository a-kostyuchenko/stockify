using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public interface ICalculator
{
    Result<CalculationResult> Calculate(Session session);
}
