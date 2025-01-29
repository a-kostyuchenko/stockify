using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Questions;

public class Answer : Entity<AnswerId>
{
    private Answer() : base(AnswerId.New())
    {
    }
    
    public string Content { get; private set; }
    public int Points { get; private set; }
    public string Explanation { get; private set; } = string.Empty;
    public QuestionId QuestionId { get; private set; }
    
    public static Answer Create(QuestionId questionId, string content, int points, string? explanation = null)
    {
        return new Answer
        {
            Content = content,
            Points = points,
            QuestionId = questionId,
            Explanation = explanation ?? string.Empty
        };
    }
}
