namespace Stockify.Modules.Risks.Domain.Questions;

public interface IQuestionRepository
{
    Task<Question?> GetAsync(QuestionId id, CancellationToken cancellationToken = default);
    Task<List<Question>> GetRandomAsync(int questionsCount, CancellationToken cancellationToken = default);
    void Insert(Question question);
}
