using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public static class FormulaErrors
{
    public static readonly Error NoCategories = Error.Problem(
        "Formula.NoCategories",
        "No categories available");
    
    public static readonly Error NoData = Error.Problem(
        "Formula.NoData",
        "Not enough data available");
}