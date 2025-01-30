using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public interface IFormula
{
    string Name { get; }
    string Description { get; }
    Result<decimal> Calculate(SessionScores scores);
}
