using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions.Events;

namespace Stockify.Modules.Risks.Domain.Sessions;

public class Session : Entity<SessionId>
{
    private Session()
        : base(SessionId.New()) { }

    public const int MaxQuestionsCount = 100;
    public const int MinQuestionsCount = 20;

    private readonly HashSet<Question> _questions = [];
    private readonly HashSet<Submission> _submissions = [];

    public IndividualId IndividualId { get; private set; }
    public DateTime? StartedAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }
    public SessionStatus Status { get; private set; }
    public int TotalPoints { get; private set; }
    public int MaxPoints { get; private set; }

    public IReadOnlyCollection<Question> Questions => [.. _questions];
    public IReadOnlyCollection<Submission> Submissions => [.. _submissions];

    internal static Session Create(IndividualId individualId)
    {
        var session = new Session { IndividualId = individualId, Status = SessionStatus.Draft };

        session.Raise(new SessionCreatedDomainEvent(session.Id));

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

        if (Status != SessionStatus.Draft)
        {
            return Result.Failure(SessionErrors.InvalidStatus);
        }

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

        if (_questions.Count < MinQuestionsCount)
        {
            return Result.Failure(SessionErrors.NotEnoughQuestions);
        }

        StartedAtUtc = utcNow;
        Status = SessionStatus.Active;
        MaxPoints = _questions.Select(q => q.Answers.Max(a => a.Points)).Sum();

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

    public Result Complete(DateTime utcNow)
    {
        if (Status != SessionStatus.Active)
        {
            return Result.Failure(SessionErrors.InvalidStatus);
        }

        if (!AllQuestionsAnswered())
        {
            return Result.Failure(SessionErrors.IncompleteSubmissions);
        }

        Status = SessionStatus.Completed;
        CompletedAtUtc = utcNow;
        TotalPoints = _submissions.Sum(s => s.Points);

        Raise(new SessionCompletedDomainEvent(Id));

        return Result.Success();
    }

    private bool AllQuestionsAnswered()
    {
        var submittedQuestionIds = new HashSet<QuestionId>(_submissions.Select(s => s.QuestionId));
        return _questions.All(q => submittedQuestionIds.Contains(q.Id));
    }
}
