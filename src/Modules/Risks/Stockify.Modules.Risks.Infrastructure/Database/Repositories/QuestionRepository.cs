using Microsoft.EntityFrameworkCore;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Modules.Risks.Domain.Questions;

namespace Stockify.Modules.Risks.Infrastructure.Database.Repositories;

internal sealed class QuestionRepository(RisksDbContext dbContext) : IQuestionRepository, IScoped
{
    public async Task<Question?> GetAsync(QuestionId id, CancellationToken cancellationToken = default) => 
        await dbContext.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == id, cancellationToken);

    public async Task<List<Question>> GetRandomAsync(
        int questionsCount,
        CancellationToken cancellationToken = default)
    {
        const int buffer = 50;
        
        List<Question> questions = await dbContext.Questions
            .Where(q => q.Answers.Count >= Question.MinAnswersCount)
            .Include(q => q.Answers)
            .OrderBy(_ => Guid.NewGuid())
            .Take(questionsCount + buffer)
            .ToListAsync(cancellationToken);

        for (int i = questions.Count - 1; i > 0; i--)
        {
#pragma warning disable CA5394
            int j = Random.Shared.Next(i + 1);
#pragma warning restore CA5394
            (questions[i], questions[j]) = (questions[j], questions[i]);
        }

        return questions.Take(questionsCount).ToList();
    }

    public void Insert(Question question) => 
        dbContext.Questions.Add(question);
}
