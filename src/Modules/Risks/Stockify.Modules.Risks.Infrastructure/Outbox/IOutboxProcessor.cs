namespace Stockify.Modules.Risks.Infrastructure.Outbox;

public interface IOutboxProcessor
{
    Task ProcessAsync();
}
