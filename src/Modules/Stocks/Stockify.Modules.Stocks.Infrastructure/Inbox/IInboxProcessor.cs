namespace Stockify.Modules.Stocks.Infrastructure.Inbox;

public interface IInboxProcessor
{
    Task ProcessAsync();
}
