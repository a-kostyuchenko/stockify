using System.Data.Common;
using Dapper;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetQuestions;

internal sealed class GetSessionQuestionsQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetSessionQuestionsQuery, GetSessionQuestionsResponse>
{
    public async Task<Result<GetSessionQuestionsResponse>> Handle(
        GetSessionQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        var parameters = new GetQuestionsParameters(
            request.SessionId.Value,
            request.IndividualId.Value,
            request.PageSize,
            (request.Page - 1) * request.PageSize);
        
        IReadOnlyCollection<QuestionResponse> questions = await GetQuestionsAsync(connection, parameters);
        
        int totalCount = await CountQuestionsAsync(connection, parameters);
        
        return new GetSessionQuestionsResponse(request.Page, request.PageSize, totalCount, questions);
    }
    
    private static async Task<IReadOnlyCollection<QuestionResponse>> GetQuestionsAsync(
        DbConnection connection,
        GetQuestionsParameters parameters)
    {
        const string sql = 
            $"""
             SELECT
                 q.id AS {nameof(QuestionResponse.Id)},
                 q.content AS {nameof(QuestionResponse.Content)},
                 a.id AS {nameof(AnswerResponse.AnswerId)},
                 a.content AS {nameof(AnswerResponse.Content)}
                FROM risks.questions q
                JOIN risks.session_questions sq ON q.id = sq.questions_id
                JOIN risks.sessions s on s.id = sq.session_id
                LEFT JOIN risks.answers a ON a.question_id = q.id
                WHERE sq.session_id = @SessionId AND s.individual_id = @IndividualId
                ORDER BY q.content
                OFFSET @Skip
                LIMIT @Take
             """;

        List<QuestionResponse> questions = (await connection.QueryAsync<QuestionResponse, AnswerResponse?, QuestionResponse>(
            sql,
            (question, answer) =>
            {
                if (answer is not null)
                {
                    question.Answers.Add(answer);
                }

                return question;
            },
            parameters,
            splitOn: nameof(AnswerResponse.AnswerId))).AsList();

        return questions;
    }
    
    private static async Task<int> CountQuestionsAsync(DbConnection connection, GetQuestionsParameters parameters)
    {
        const string sql =
            """
            SELECT COUNT(*)
            FROM risks.questions q
            JOIN risks.session_questions sq ON q.id = sq.questions_id
            JOIN risks.sessions s on s.id = sq.session_id
            WHERE sq.session_id = @SessionId AND s.individual_id = @IndividualId
            """;

        int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return totalCount;
    }
}
