using System.Data.Common;
using Dapper;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Application.Pagination;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Application.Sessions.Queries.GetById;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.Get;

internal sealed class GetSessionsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetSessionsQuery, PagedResponse<SessionResponse>>
{
    public async Task<Result<PagedResponse<SessionResponse>>> Handle(
        GetSessionsQuery request,
        CancellationToken cancellationToken
    )
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var parameters = new GetSessionsParameters(
            request.IndividualId.Value,
            request.Status,
            request.StartedAtUtc,
            request.CompletedAtUtc,
            request.PageSize,
            (request.Page - 1) * request.PageSize
        );

        IReadOnlyCollection<SessionResponse> sessions = await GetSessionsAsync(
            connection,
            parameters
        );

        int totalCount = await CountSessionsAsync(connection, parameters);

        return new PagedResponse<SessionResponse>(
            request.Page,
            request.PageSize,
            totalCount,
            sessions
        );
    }

    private static async Task<int> CountSessionsAsync(
        DbConnection connection,
        GetSessionsParameters parameters
    )
    {
        const string sql = """
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
        GetSessionsParameters parameters
    )
    {
        const string sql = $"""
            SELECT
                id AS {nameof(SessionResponse.Id)},
                individual_id AS {nameof(SessionResponse.IndividualId)},
                started_at_utc AS {nameof(SessionResponse.StartedAtUtc)},
                completed_at_utc AS {nameof(SessionResponse.CompletedAtUtc)},
                status AS {nameof(SessionResponse.Status)}
            FROM risks.sessions
            WHERE
                individual_id = @IndividualId AND
                (@Status IS NULL OR status = @Status) AND
                (@StartedAtUtc::timestamp IS NULL OR started_at_utc >= @StartedAtUtc::timestamp) AND
                (@CompletedAtUtc::timestamp IS NULL OR completed_at_utc >= @CompletedAtUtc::timestamp)
            ORDER BY started_at_utc
            OFFSET @Skip
            LIMIT @Take
            """;

        List<SessionResponse> sessions = (
            await connection.QueryAsync<SessionResponse>(sql, parameters)
        ).AsList();

        return sessions;
    }
}
