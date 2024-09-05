using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Data;
using Stockify.Modules.Stocks.Domain.Stockholders;

namespace Stockify.Modules.Stocks.Application.Stockholders.Commands.Create;

internal sealed class CreateStockholderCommandHandler(
    IStockholderRepository stockholderRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateStockholderCommand>
{
    public async Task<Result> Handle(CreateStockholderCommand request, CancellationToken cancellationToken)
    {
        var stockholder = Stockholder.Create(request.Id, request.FirstName, request.LastName, request.Email);
        
        stockholderRepository.Insert(stockholder);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}