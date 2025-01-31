using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed record CalculationResult(
    RiskAttitude Attitude,
    FormulaType FormulaType,
    HashSet<QuestionCategory> UsedCategories);
