using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Data;
using Stockify.Modules.Stocks.Domain.TickerTypes;

namespace Stockify.Modules.Stocks.Application.TickerTypes.Commands.Create;

internal sealed class CreateTickerTypeCommandHandler(
    ITickerTypeRepository tickerTypeRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateTickerTypeCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTickerTypeCommand request, CancellationToken cancellationToken)
    {
        if (!await tickerTypeRepository.IsCodeUniqueAsync(request.Code, cancellationToken))
        {
            return Result.Failure<Guid>(TickerTypeErrors.CodeIsNotUnique);
        }
        
        var tickerType = TickerType.Create(request.Code, request.Description);
        
        tickerTypeRepository.Insert(tickerType);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return tickerType.Id.Value;
    }
}