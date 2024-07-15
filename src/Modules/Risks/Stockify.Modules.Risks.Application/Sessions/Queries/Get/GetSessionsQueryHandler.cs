using System.Data.Common;
using Dapper;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Application.Abstractions.Authentication;
using Stockify.Modules.Risks.Application.Sessions.Queries.GetById;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.Get;

internal sealed class GetSessionsQueryHandler(
    IDbConnectionFactory dbConnectionFactory,
    IIndividualContext individualContext) : IQueryHandler<GetSessionsQuery, GetSessionsResponse>
{
    public async Task<Result<GetSessionsResponse>> Handle(
        GetSessionsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        var parameters = new GetSessionsParameters(
            individualContext.IndividualId.Value,
            request.Status,
            request.StartedAtUtc,
            request.PageSize,
            (request.Page - 1) * request.PageSize);
        
        IReadOnlyCollection<SessionResponse> sessions = await GetSessionsAsync(connection, parameters);
        
        int totalCount = await CountSessionsAsync(connection, parameters);
        
        return new GetSessionsResponse(request.Page, request.PageSize, totalCount, sessions);
    }

    private static async Task<int> CountSessionsAsync(DbConnection connection, GetSessionsParameters parameters)
    {
        const string sql =
            """
            SELECT COUNT(*)
            FROM risks.sessions
            WHERE
                individual_id = @IndividualId AND
                (@Status IS NULL OR status = @Status) AND
                (@StartedAtUtc::timestamp IS NULL OR started_at_utc >= @StartedAtUtc::timestamp)
            """;
        
        int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);
        
        return totalCount;
    }

    private static async Task<IReadOnlyCollection<SessionResponse>> GetSessionsAsync(
        DbConnection connection,
        GetSessionsParameters parameters)
    {
        const string sql =
            $"""
             SELECT
                 id AS {nameof(SessionResponse.Id)},
                 individual_id AS {nameof(SessionResponse.IndividualId)},
                 status AS {nameof(SessionResponse.Status)},
                 started_at_utc AS {nameof(SessionResponse.StartedAtUtc)}
             FROM risks.sessions
             WHERE
                 individual_id = @IndividualId AND
                 (@Status IS NULL OR status = @Status) AND
                 (@StartedAtUtc::timestamp IS NULL OR started_at_utc >= @StartedAtUtc::timestamp)
             ORDER BY started_at_utc
             OFFSET @Skip
             LIMIT @Take
             """;
        
        List<SessionResponse> sessions = (await connection.QueryAsync<SessionResponse>(
                sql,
                parameters))
            .AsList();

        return sessions;
    }
}
