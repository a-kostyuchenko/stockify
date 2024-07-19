namespace Stockify.Modules.Stocks.Infrastructure.Outbox;

public interface IOutboxProcessor
{
    Task ProcessAsync();
}
