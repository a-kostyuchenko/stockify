using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions.Events;

namespace Stockify.Modules.Risks.Domain.Sessions;

public class Session : Entity<SessionId>
{
    private Session()
        : base(SessionId.New()) { }

    private readonly HashSet<Question> _questions = [];
    private readonly HashSet<Submission> _submissions = [];

    public IndividualId IndividualId { get; private set; }
    public DateTime? StartedAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }
    public SessionStatus Status { get; private set; }
    public int TotalPoints { get; private set; }
    public int MaxPoints { get; private set; }
    public decimal CompletionPercentage => _submissions.Count * 100m / _questions.Count;

    public IReadOnlyCollection<Question> Questions => [.. _questions];
    public IReadOnlyCollection<Submission> Submissions => [.. _submissions];

    internal static Session Create(IndividualId individualId)
    {
        var session = new Session { IndividualId = individualId, Status = SessionStatus.Draft };

        session.Raise(new SessionCreatedDomainEvent(session.Id));

        return session;
    }

    internal Result AddQuestion(Question question, SessionPolicy policy)
    {
        if (_questions.Count >= policy.MaxQuestionsCount)
        {
            return Result.Failure(SessionErrors.MaxQuestionsExceeded);
        }

        if (question.Answers.Count < Question.MinAnswersPerQuestion)
        {
            return Result.Failure(QuestionErrors.NotEnoughAnswers);
        }

        if (Status != SessionStatus.Draft)
        {
            return Result.Failure(SessionErrors.InvalidStatus);
        }

        MaxPoints += question.GetMaxPoints();

        _questions.Add(question);

        return Result.Success();
    }

    public Result Start(DateTime utcNow)
    {
        if (Status != SessionStatus.Draft)
        {
            return Result.Failure(SessionErrors.InvalidStatus);
        }

        if (Status == SessionStatus.Active)
        {
            return Result.Failure(SessionErrors.AlreadyStarted);
        }

        StartedAtUtc = utcNow;
        Status = SessionStatus.Active;

        Raise(new SessionStartedDomainEvent(Id));

        return Result.Success();
    }

    public Result SubmitAnswer(QuestionId questionId, AnswerId answerId)
    {
        if (Status != SessionStatus.Active)
        {
            return Result.Failure(SessionErrors.InvalidStatus);
        }

        Question? question = _questions.FirstOrDefault(q => q.Id == questionId);

        if (question is null)
        {
            return Result.Failure(QuestionErrors.NotFound);
        }

        Answer? answer = question.Answers.FirstOrDefault(a => a.Id == answerId);

        if (answer is null)
        {
            return Result.Failure(AnswerErrors.NotFound);
        }

        var submission = Submission.Submit(this, question, answer);
        _submissions.Add(submission);

        return Result.Success();
    }

    public Result Complete(DateTime utcNow, SessionPolicy policy)
    {
        if (Status != SessionStatus.Active)
        {
            return Result.Failure(SessionErrors.InvalidStatus);
        }

        if (!AllQuestionsAnswered())
        {
            return Result.Failure(SessionErrors.IncompleteSubmissions);
        }
        
        TimeSpan duration = utcNow - StartedAtUtc!.Value;
        
        if (duration > policy.MaxDuration)
        {
            return Result.Failure(SessionErrors.TimeoutExceeded);
        }

        Status = SessionStatus.Completed;
        CompletedAtUtc = utcNow;
        TotalPoints = _submissions.Sum(s => s.Points);

        Raise(new SessionCompletedDomainEvent(Id));

        return Result.Success();
    }

    private bool AllQuestionsAnswered() => 
        _questions.All(q => _submissions.Any(s => s.QuestionId == q.Id));
    
    public IDictionary<QuestionCategory, (int Total, int Max)> GetScoresByCategory()
    {
        return _submissions
            .GroupBy(s => _questions.First(q => q.Id == s.QuestionId).Category)
            .ToDictionary(
                g => g.Key,
                g => (
                    Total: g.Sum(s => s.Points),
                    Max: g.Sum(s => _questions.First(q => q.Id == s.QuestionId).GetMaxPoints()))
            );
    }
    
    public IDictionary<QuestionCategory, int> GetQuestionDistribution() =>
        _questions
            .GroupBy(q => q.Category)
            .ToDictionary(g => g.Key, g => g.Count());
}
