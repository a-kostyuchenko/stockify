using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Domain.Sessions;

public class Session : Entity<SessionId>
{
    private Session() : base(SessionId.New())
    {
    }
    
    public const int MaxQuestionsCount = 20;
    public const int MinQuestionsCount = 5;

    private readonly HashSet<Question> _questions = [];

    public IndividualId IndividualId { get; private set; }
    public DateTime? StartedAtUtc { get; private set; }
    public SessionStatus Status { get; private set; }
    
    public IReadOnlyCollection<Question> Questions => _questions.ToList();

    internal static Session Create(IndividualId individualId)
    {
        var session = new Session
        {
            IndividualId = individualId,
            Status = SessionStatus.Draft
        };

        return session;
    }
    
    internal Result AddQuestion(Question question)
    {
        if (_questions.Count >= MaxQuestionsCount)
        {
            return Result.Failure(SessionErrors.MaxQuestionsExceeded);
        }

        if (question.Answers.Count < Question.MinAnswersCount)
        {
            return Result.Failure(QuestionErrors.NotEnoughAnswers);
        }
        
        _questions.Add(question);
        
        return Result.Success();
    }
}
