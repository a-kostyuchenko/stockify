using Stockify.Modules.Risks.Domain.Individuals;

namespace Stockify.Modules.Risks.Application.Abstractions.Authentication;

public interface IIndividualContext
{
    IndividualId IndividualId { get; }
}
