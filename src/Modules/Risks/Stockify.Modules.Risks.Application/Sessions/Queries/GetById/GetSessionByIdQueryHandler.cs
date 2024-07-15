using System.Data.Common;
using Dapper;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetById;

internal sealed class GetSessionByIdQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetSessionByIdQuery, SessionResponse>
{
    public async Task<Result<SessionResponse>> Handle(
        GetSessionByIdQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = $"""
                            SELECT
                                s.id AS {nameof(SessionResponse.Id)},
                                s.individual_id AS {nameof(SessionResponse.IndividualId)},
                                s.started_at_utc AS {nameof(SessionResponse.StartedAtUtc)},
                                s.completed_at_utc AS {nameof(SessionResponse.CompletedAtUtc)},
                                s.status AS {nameof(SessionResponse.Status)}
                            FROM risks.sessions s
                            WHERE s.id = @SessionId
                            """;
        
        SessionResponse? session = await connection.QueryFirstOrDefaultAsync<SessionResponse>(
            sql,
            new { SessionId = request.SessionId.Value });

        if (session is null)
        {
            return Result.Failure<SessionResponse>(SessionErrors.NotFound);
        }

        return session;
    }
}
