using System.Data.Common;
using Dapper;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetResult;

internal sealed class GetSessionResultQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetSessionResultQuery, SessionResultResponse>
{
    public async Task<Result<SessionResultResponse>> Handle(
        GetSessionResultQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = 
            $"""
             SELECT
                 s.id,
                 s.started_at_utc,
                 s.completed_at_utc,
                 s.total_points,
                 s.max_points
             FROM risks.sessions s
             WHERE s.id = @SessionId AND
                   s.individual_id = @IndividualId AND
                   s.status = @Status
             """;

        SessionResultResponse? result = await connection.QueryFirstOrDefaultAsync<SessionResultResponse>(
            sql,
            new
            {
                SessionId = request.SessionId.Value,
                IndividualId = request.IndividualId.Value,
                Status = SessionStatus.Completed.Name
            });

        if (result is null)
        {
            return Result.Failure<SessionResultResponse>(SessionErrors.NotFound);
        }

        return result;
    }
}
