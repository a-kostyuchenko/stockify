using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public interface IFormulaSelector
{
    Result<IFormula> Select(IReadOnlySet<QuestionCategory> availableCategories);
}