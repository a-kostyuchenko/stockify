using Microsoft.EntityFrameworkCore;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Infrastructure.Database.Repositories;

internal sealed class QuestionRepository(RisksDbContext dbContext) : IQuestionRepository, IScoped
{
    public async Task<Question?> GetAsync(QuestionId id, CancellationToken cancellationToken = default) => 
        await dbContext.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);

    public async Task<List<Question>> GetDistributedAsync(
        int questionsCount,
        SessionPolicy policy,
        CancellationToken cancellationToken = default)
    {
        Dictionary<QuestionCategory, int> distribution = CalculateDistribution(
            questionsCount,
            policy.CategoryWeights);
        
        var questions = new List<Question>();

        foreach ((QuestionCategory category, int count) in distribution)
        {
            List<Question> categoryQuestions = await dbContext.Questions
                .Include(q => q.Answers)
                .Where(q => q.Category == category)
                .OrderBy(q => EF.Functions.Random())
                .Take(count)
                .ToListAsync(cancellationToken);

            // Ensure we got enough questions for this category
            if (categoryQuestions.Count < policy.MinQuestionsPerCategory)
            {
                // Not enough questions available for this category
                continue;
            }

            questions.AddRange(categoryQuestions);
        }
        
        if (!ValidateDistribution(questions, policy))
        {
            await CompensateDistribution(
                questions, 
                questionsCount, 
                policy, 
                cancellationToken);
        }

        return questions;
    }

    public void Insert(Question question) => 
        dbContext.Questions.Add(question);
    
    private static Dictionary<QuestionCategory, int> CalculateDistribution(
        int totalQuestions,
        IReadOnlyDictionary<QuestionCategory, int> weights)
    {
        var distribution = new Dictionary<QuestionCategory, int>();
        int totalWeight = weights.Values.Sum();
        int remainingQuestions = totalQuestions;
        
        foreach ((QuestionCategory category, int weight) in weights.OrderByDescending(w => w.Value))
        {
            if (category == weights.Keys.Last())
            {
                distribution[category] = remainingQuestions;
            }
            else
            {
                int categoryQuestions = (int)Math.Ceiling(
                    (decimal)totalQuestions * weight / totalWeight);
                    
                categoryQuestions = Math.Min(categoryQuestions, remainingQuestions);
                
                distribution[category] = categoryQuestions;
                remainingQuestions -= categoryQuestions;
            }
        }

        return distribution;
    }
    
    private static bool ValidateDistribution(
        List<Question> questions, 
        SessionPolicy policy)
    {
        var categoryCounts = questions
            .GroupBy(q => q.Category)
            .ToDictionary(g => g.Key, g => g.Count());

        foreach (QuestionCategory category in policy.CategoryWeights.Keys)
        {
            if (!categoryCounts.TryGetValue(category, out int count) || 
                count < policy.MinQuestionsPerCategory)
            {
                return false;
            }
        }

        return categoryCounts.Count >= policy.RequiredCategoryCount;
    }
    
    private async Task CompensateDistribution(
        List<Question> currentQuestions,
        int targetCount,
        SessionPolicy policy,
        CancellationToken cancellationToken)
    {
        var missingCategories = policy.CategoryWeights.Keys
            .Except(currentQuestions.Select(q => q.Category))
            .ToList();

        foreach (QuestionCategory category in missingCategories)
        {
            List<Question> additionalQuestions = await dbContext.Questions
                .Include(q => q.Answers)
                .Where(q => q.Category == category)
                .OrderBy(q => EF.Functions.Random())
                .Take(policy.MinQuestionsPerCategory)
                .ToListAsync(cancellationToken);

            currentQuestions.AddRange(additionalQuestions);
        }

        int remainingCount = targetCount - currentQuestions.Count;
        
        if (remainingCount > 0)
        {
            var existingIds = currentQuestions.Select(q => q.Id).ToList();
            
            List<Question> additionalQuestions = await dbContext.Questions
                .Include(q => q.Answers)
                .Where(q => !existingIds.Contains(q.Id))
                .OrderBy(q => EF.Functions.Random())
                .Take(remainingCount)
                .ToListAsync(cancellationToken);

            currentQuestions.AddRange(additionalQuestions);
        }

        while (currentQuestions.Count > targetCount)
        {
            var categoryCounts = currentQuestions
                .GroupBy(q => q.Category)
                .ToDictionary(g => g.Key, g => g.Count());

            QuestionCategory categoryToRemoveFrom = categoryCounts
                .Where(kvp => kvp.Value > policy.MinQuestionsPerCategory)
                .OrderByDescending(kvp => kvp.Value)
                .First()
                .Key;

            Question questionToRemove = currentQuestions
                .Where(q => q.Category == categoryToRemoveFrom)
                .OrderBy(_ => EF.Functions.Random())
                .First();

            currentQuestions.Remove(questionToRemove);
        }
    }
}
