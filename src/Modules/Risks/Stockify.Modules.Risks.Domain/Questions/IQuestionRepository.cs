using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Questions;

public interface IQuestionRepository
{
    Task<Question?> GetAsync(QuestionId id, CancellationToken cancellationToken = default);
    Task<List<Question>> GetDistributedAsync(
        int questionsCount,
        SessionPolicy policy,
        CancellationToken cancellationToken = default);
    void Insert(Question question);
}
