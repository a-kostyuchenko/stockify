using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public static class IndividualErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Individual.NotFound",
        "The individual with specified identifier was not found");
}
