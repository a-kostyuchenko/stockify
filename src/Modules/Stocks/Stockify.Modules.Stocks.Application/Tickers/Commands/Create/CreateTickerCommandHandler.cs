using Microsoft.EntityFrameworkCore;
using Npgsql;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Application.Abstractions.Data;
using Stockify.Modules.Stocks.Domain.Tickers;
using Stockify.Modules.Stocks.Domain.TickerTypes;

namespace Stockify.Modules.Stocks.Application.Tickers.Commands.Create;

internal sealed class CreateTickerCommandHandler(
    ITickerRepository tickerRepository,
    ITickerTypeRepository tickerTypeRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateTickerCommand, string>
{
    public async Task<Result<string>> Handle(CreateTickerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            TickerType? tickerType = await tickerTypeRepository.GetAsync(request.TickerTypeId, cancellationToken);

            if (tickerType is null)
            {
                return Result.Failure<string>(TickerTypeErrors.NotFound);
            }

            var ticker = Ticker.Create(request.Symbol, request.Name, request.Description, request.Cik, tickerType);

            tickerRepository.Insert(ticker);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ticker.Id.Value;
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException is NpgsqlException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            return Result.Failure<string>(TickerErrors.IsNotUnique);
        }
    }
}
