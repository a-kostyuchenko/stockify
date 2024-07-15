using System.Data.Common;
using Dapper;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Application.Questions.Queries.Get;

internal sealed class GetQuestionsQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetQuestionsQuery, GetQuestionsResponse>
{
    public async Task<Result<GetQuestionsResponse>> Handle(
        GetQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var parameters = new GetQuestionsParameters(
            $"%{request.SearchTerm}%",
            request.PageSize,
            (request.Page - 1) * request.PageSize);
        
        IReadOnlyCollection<QuestionResponse> questions = await GetQuestionsAsync(connection, parameters);
        
        int totalCount = await CountQuestionsAsync(connection, parameters);
        
        return new GetQuestionsResponse(request.Page, request.PageSize, totalCount, questions);
    }

    private static async Task<IReadOnlyCollection<QuestionResponse>> GetQuestionsAsync(
        DbConnection connection,
        GetQuestionsParameters parameters)
    {
        const string sql = 
            $"""
             SELECT
                 q.id AS {nameof(QuestionResponse.Id)},
                 q.content AS {nameof(QuestionResponse.Content)}
             FROM risks.questions q
             WHERE (@SearchTerm IS NULL OR q.content ILIKE @SearchTerm)
             ORDER BY q.content
             OFFSET @Skip
             LIMIT @Take
             """;

        List<QuestionResponse> questions = (await connection.QueryAsync<QuestionResponse>(
                sql,
                parameters))
            .AsList();

        return questions;
    }
    
    private static async Task<int> CountQuestionsAsync(DbConnection connection, GetQuestionsParameters parameters)
    {
        const string sql =
            """
            SELECT COUNT(*)
            FROM risks.questions q
            WHERE (@SearchTerm IS NULL OR q.content ILIKE @SearchTerm)
            """;

        int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return totalCount;
    }
}
