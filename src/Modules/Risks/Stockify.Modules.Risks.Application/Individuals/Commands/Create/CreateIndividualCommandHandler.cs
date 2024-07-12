using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Risks.Application.Abstractions.Data;
using Stockify.Modules.Risks.Domain.Individuals;

namespace Stockify.Modules.Risks.Application.Individuals.Commands.Create;

internal sealed class CreateIndividualCommandHandler(
    IIndividualRepository individualRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateIndividualCommand>
{
    public async Task<Result> Handle(CreateIndividualCommand request, CancellationToken cancellationToken)
    {
        var individual = Individual.Create(request.Id, request.FirstName, request.LastName, request.Email);
        
        individualRepository.Insert(individual);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
