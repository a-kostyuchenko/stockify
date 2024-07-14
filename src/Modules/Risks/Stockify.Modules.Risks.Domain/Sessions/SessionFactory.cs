using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Domain.Sessions;

public sealed class SessionFactory(IQuestionRepository questionRepository) : ISessionFactory
{
    public async Task<Result<Session>> CreateAsync(
        IndividualId individualId,
        int questionsCount,
        CancellationToken cancellationToken = default)
    {
        if (questionsCount is < Session.MinQuestionsCount or > Session.MaxQuestionsCount)
        {
            return Result.Failure<Session>(SessionErrors.InvalidQuestionsCount);
        }
        
        var session = Session.Create(individualId);
        
        List<Question> questions = await questionRepository.GetRandomAsync(
            questionsCount,
            cancellationToken);

        if (questions.Count != questionsCount)
        {
            return Result.Failure<Session>(SessionErrors.NotEnoughQuestions);
        }

        var results = questions.Select(question => session.AddQuestion(question)).ToList();

        if (results.Any(r => r.IsFailure))
        {
            return Result.Failure<Session>(ValidationError.FromResults(results));
        }

        return session;
    }
}
