using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Domain.Sessions;

public sealed record SessionPolicy
{
    private SessionPolicy(
        int minQuestionsCount,
        int maxQuestionsCount,
        int requiredCategoryCount,
        int minQuestionsPerCategory,
        TimeSpan maxDuration,
        Dictionary<QuestionCategory, int> categoryWeights)
    {
        MinQuestionsCount = minQuestionsCount;
        MaxQuestionsCount = maxQuestionsCount;
        RequiredCategoryCount = requiredCategoryCount;
        MinQuestionsPerCategory = minQuestionsPerCategory;
        MaxDuration = maxDuration;
        CategoryWeights = categoryWeights;
    }
    
    public int MinQuestionsCount { get; }
    public int MaxQuestionsCount { get; }
    public int RequiredCategoryCount { get; }
    public int MinQuestionsPerCategory { get; }
    public TimeSpan MaxDuration { get; }
    public IReadOnlyDictionary<QuestionCategory, int> CategoryWeights { get; }
    
    public static SessionPolicy Default => new(
        minQuestionsCount: 10,
        maxQuestionsCount: 30,
        requiredCategoryCount: 4,
        minQuestionsPerCategory: 2,
        maxDuration: TimeSpan.FromMinutes(30),
        categoryWeights: new Dictionary<QuestionCategory, int>
        {
            { QuestionCategory.RiskTolerance, 3 },
            { QuestionCategory.LossTolerance, 3 },
            { QuestionCategory.InvestmentHorizon, 2 },
            { QuestionCategory.IncomeStability, 1 },
            { QuestionCategory.FinancialKnowledge, 1 },
            { QuestionCategory.InvestmentExperience, 1 }
        });
    
    public Result Validate(Session session)
    {
        List<Error> errors = [];

        ValidateQuestionsCount(session, errors);
        ValidateCategoryDistribution(session, errors);
        ValidateDuration(session, errors);
        ValidateCompleteness(session, errors);

        return errors.Any() 
            ? Result.Failure(ValidationError.FromErrors([.. errors])) 
            : Result.Success();
    }

    private void ValidateQuestionsCount(Session session, List<Error> errors)
    {
        int questionCount = session.Questions.Count;
        
        if (questionCount < MinQuestionsCount)
        {
            errors.Add(SessionErrors.NotEnoughQuestions);
        }
        
        if (questionCount > MaxQuestionsCount)
        {
            errors.Add(SessionErrors.MaxQuestionsExceeded);
        }
    }

    private void ValidateCategoryDistribution(Session session, List<Error> errors)
    {
        var categoryCounts = session.Questions
            .GroupBy(q => q.Category)
            .ToDictionary(g => g.Key, g => g.Count());

        if (categoryCounts.Count < RequiredCategoryCount)
        {
            errors.Add(SessionErrors.InsufficientCategories);
        }

        foreach (QuestionCategory category in CategoryWeights.Keys)
        {
            if (!categoryCounts.TryGetValue(category, out int count) || 
                count < MinQuestionsPerCategory)
            {
                errors.Add(SessionErrors.IncorrectCategoryDistribution(category));
            }
        }
    }

    private void ValidateDuration(Session session, List<Error> errors)
    {
        if (session is { StartedAtUtc: not null, CompletedAtUtc: not null })
        {
            TimeSpan duration = session.CompletedAtUtc.Value - session.StartedAtUtc.Value;
            
            if (duration > MaxDuration)
            {
                errors.Add(SessionErrors.TimeoutExceeded);
            }
        }
    }

    private static void ValidateCompleteness(Session session, List<Error> errors)
    {
        if (session.Status == SessionStatus.Completed && 
            session.Submissions.Count != session.Questions.Count)
        {
            errors.Add(SessionErrors.IncompleteSubmissions);
        }
    }
}
