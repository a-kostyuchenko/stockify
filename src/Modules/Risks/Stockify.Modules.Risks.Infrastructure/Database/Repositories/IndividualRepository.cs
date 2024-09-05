using Microsoft.EntityFrameworkCore;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Modules.Risks.Domain.Individuals;

namespace Stockify.Modules.Risks.Infrastructure.Database.Repositories;

internal sealed class IndividualRepository(RisksDbContext dbContext) : IIndividualRepository, IScoped
{
    public async Task<Individual?> GetAsync(IndividualId id, CancellationToken cancellationToken = default) => 
        await dbContext.Individuals.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

    public void Insert(Individual individual) => 
        dbContext.Individuals.Add(individual);
}
