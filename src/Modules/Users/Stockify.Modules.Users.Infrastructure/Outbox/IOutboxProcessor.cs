namespace Stockify.Modules.Users.Infrastructure.Outbox;

public interface IOutboxProcessor
{
    Task ProcessAsync();
}
