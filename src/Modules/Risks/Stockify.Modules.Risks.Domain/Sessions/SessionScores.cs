using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Domain.Sessions;

public sealed record SessionScores
{
    private readonly IReadOnlyDictionary<QuestionCategory, (int Total, int Max)> _categoryScores;
    
    public SessionScores(IDictionary<QuestionCategory, (int Total, int Max)> categoryScores)
    {
        _categoryScores = categoryScores.AsReadOnly();
    }

    private (int Total, int Max) GetCategoryScore(QuestionCategory category) =>
        _categoryScores.GetValueOrDefault(category);
        
    public decimal GetCategoryCoefficient(QuestionCategory category)
    {
        (int total, int max) = GetCategoryScore(category);
        return max > 0 ? (decimal)total / max : 0;
    }
}
