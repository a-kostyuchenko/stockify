using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Domain.Sessions;

public sealed class Submission : Entity<SubmissionId>
{
    private Submission() : base(SubmissionId.New())
    {
    }
    
    public SessionId SessionId { get; init; }
    public QuestionId QuestionId { get; init; }
    public AnswerId AnswerId { get; init; }
    public int Points { get; init; }

    internal static Submission Submit(Session session, Question question, Answer answer)
    {
        return new Submission
        {
            SessionId = session.Id,
            QuestionId = question.Id,
            AnswerId = answer.Id,
            Points = answer.Points
        };
    }
}

public record struct SubmissionId(Guid Value) : IEntityId<SubmissionId>
{
    public static SubmissionId Empty => new(Guid.Empty);
    public static SubmissionId New() => new(Guid.NewGuid());
    public static SubmissionId From(Guid value) => new(value);
}
