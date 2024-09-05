using Microsoft.EntityFrameworkCore;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Infrastructure.Database.Repositories;

internal sealed class SessionRepository(RisksDbContext dbContext) : ISessionRepository, IScoped
{
    public async Task<Session?> GetAsync(SessionId id, CancellationToken cancellationToken = default) => 
        await dbContext.Sessions
            .Include(s => s.Questions).ThenInclude(q => q.Answers)
            .Include(s => s.Submissions)
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public void Insert(Session session) => 
        dbContext.Sessions.Add(session);
}
