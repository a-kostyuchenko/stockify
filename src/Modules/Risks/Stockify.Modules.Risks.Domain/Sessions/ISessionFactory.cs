using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Individuals;

namespace Stockify.Modules.Risks.Domain.Sessions;

public interface ISessionFactory
{
    Task<Result<Session>> CreateAsync(
        IndividualId individualId,
        int questionsCount,
        CancellationToken cancellationToken = default);
}
