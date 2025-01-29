using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Domain.Sessions;

public sealed class SessionFactory(IQuestionRepository questionRepository) : ISessionFactory
{
    public async Task<Result<Session>> CreateAsync(
        IndividualId individualId,
        int questionsCount,
        SessionPolicy policy,
        CancellationToken cancellationToken = default
    )
    {
        if (questionsCount < policy.MinQuestionsCount || questionsCount > policy.MaxQuestionsCount)
        {
            return Result.Failure<Session>(SessionErrors.InvalidQuestionsCount);
        }

        var session = Session.Create(individualId);

        List<Question> questions = await questionRepository.GetDistributedAsync(
            questionsCount,
            policy,
            cancellationToken
        );

        if (questions.Count != questionsCount)
        {
            return Result.Failure<Session>(SessionErrors.NotEnoughQuestions);
        }

        var results = questions.Select(q => session.AddQuestion(q, policy)).ToList();

        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure<Session>(ValidationError.FromResults(results));
        }
        
        Result validationResult = policy.Validate(session);
        
        if (validationResult.IsFailure)
        {
            return Result.Failure<Session>(validationResult.Error);
        }

        return session;
    }
}
