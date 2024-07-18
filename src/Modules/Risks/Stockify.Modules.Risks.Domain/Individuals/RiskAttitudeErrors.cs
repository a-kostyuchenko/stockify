using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public static class RiskAttitudeErrors
{
    public static readonly Error CoefficientOutOfRange = Error.Problem(
        "RiskAttitude.CoefficientOutOfRange",
        "Risk coefficient must be between 1 and 5.");
}