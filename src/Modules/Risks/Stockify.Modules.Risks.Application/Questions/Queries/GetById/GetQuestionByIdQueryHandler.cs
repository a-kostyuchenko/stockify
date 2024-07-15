using System.Data.Common;
using Dapper;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Application.Questions.Queries.GetById;

internal sealed class GetQuestionByIdQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetQuestionByIdQuery, QuestionResponse>
{
    public async Task<Result<QuestionResponse>> Handle(
        GetQuestionByIdQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = 
            $"""
             SELECT
                 q.id AS {nameof(QuestionResponse.Id)},
                 q.content AS {nameof(QuestionResponse.Content)},
                 a.id AS {nameof(AnswerResponse.AnswerId)},
                 a.content AS {nameof(AnswerResponse.Content)},
                 a.points AS {nameof(AnswerResponse.Points)}
             FROM risks.questions q
             LEFT JOIN risks.answers a ON a.question_id = q.id
             WHERE q.id = @QuestionId
             """;
        
        Dictionary<Guid, QuestionResponse> questionsDictionary = [];

        await connection.QueryAsync<QuestionResponse, AnswerResponse?, QuestionResponse>(
            sql,
            (question, answer) =>
            {
                if (questionsDictionary.TryGetValue(question.Id, out QuestionResponse? existingQuestion))
                {
                    question = existingQuestion;
                }
                else
                {
                    questionsDictionary.Add(question.Id, question);
                }

                if (answer is not null)
                {
                    question.Answers.Add(answer);
                }
                
                return question;
            },
            new { QuestionId = request.QuestionId.Value },
            splitOn: nameof(AnswerResponse.AnswerId));
        
        if (!questionsDictionary.TryGetValue(request.QuestionId.Value, out QuestionResponse questionResponse))
        {
            return Result.Failure<QuestionResponse>(QuestionErrors.NotFound);
        }

        return questionResponse;
    }
}
