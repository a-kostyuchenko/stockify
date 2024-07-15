using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Questions;

public class Answer : Entity<AnswerId>
{
    private Answer() : base(AnswerId.New())
    {
    }
    
    public string Content { get; private set; }
    public int Points { get; private set; }
    public QuestionId QuestionId { get; private set; }
    
    public static Answer Create(QuestionId questionId, string content, int points)
    {
        return new Answer
        {
            Content = content,
            Points = points,
            QuestionId = questionId
        };
    }
}
