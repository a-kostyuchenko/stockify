using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Data;
using Stockify.Modules.Stocks.Domain.Tickers;

namespace Stockify.Modules.Stocks.Application.Tickers.Commands.Deactivate;

internal sealed class DeactivateTickerCommandHandler(
    ITickerRepository tickerRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeactivateTickerCommand>
{
    public async Task<Result> Handle(DeactivateTickerCommand request, CancellationToken cancellationToken)
    {
        Ticker? ticker = await tickerRepository.GetAsync(request.TickerId, cancellationToken);

        if (ticker is null)
        {
            return Result.Failure(TickerErrors.NotFound);
        }
        
        ticker.Deactivate();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}