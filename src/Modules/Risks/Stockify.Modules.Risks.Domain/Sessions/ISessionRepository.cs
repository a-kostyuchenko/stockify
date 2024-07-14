namespace Stockify.Modules.Risks.Domain.Sessions;

public interface ISessionRepository
{
    Task<Session?> GetAsync(SessionId id, CancellationToken cancellationToken = default);
    void Insert(Session session);
}
