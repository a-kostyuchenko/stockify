namespace Stockify.Modules.Risks.Domain.Individuals;

public interface IIndividualRepository
{
    Task<Individual?> GetAsync(IndividualId id, CancellationToken cancellationToken = default);
    
    void Insert(Individual individual);
}
