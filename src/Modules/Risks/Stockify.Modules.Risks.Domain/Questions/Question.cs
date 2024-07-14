using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Questions;

public class Question : Entity<QuestionId>
{
    public const int MinAnswersCount = 2;

    private Question() : base(QuestionId.New())
    {
    }
    
    private readonly List<Answer> _answers = [];
    
    public string Content { get; private set; }
    public IReadOnlyCollection<Answer> Answers => _answers.ToList();
    
    public static Question Create(string content)
    {
        return new Question
        {
            Content = content
        };
    }
    
    public Result AddAnswer(string content, int points)
    {
        var answer = Answer.Create(Id, content, points);
        
        _answers.Add(answer);
        
        return Result.Success();
    }
}
