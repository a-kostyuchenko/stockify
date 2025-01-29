using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Questions;

public class Question : Entity<QuestionId>
{
    public const int MinAnswersPerQuestion = 2;
    public const int MaxAnswersPerQuestion = 5;

    private Question()
        : base(QuestionId.New()) { }

    private readonly List<Answer> _answers = [];

    public string Content { get; private set; }
    public QuestionCategory Category { get; private set; }
    public int Weight { get; private set; }
    public IReadOnlyCollection<Answer> Answers => [.. _answers];

    public static Question Create(string content, QuestionCategory category, int weight = 1)
    {
        if (weight <= 0)
        {
            throw new ArgumentException("Weight must be positive", nameof(weight));
        }
        
        return new Question { Content = content, Category = category, Weight = weight };
    }

    public Result AddAnswer(string content, int points, string? explanation = null)
    {
        if (_answers.Count >= MaxAnswersPerQuestion)
        {
            return Result.Failure(QuestionErrors.TooManyAnswers);
        }
        
        var answer = Answer.Create(Id, content, points, explanation);

        _answers.Add(answer);

        return Result.Success();
    }
    
    public int GetMaxPoints() => _answers.Sum(a => a.Points) * Weight;
}
