using Stockify.Common.Domain;
using Stockify.Modules.Stocks.Domain.Stockholders;

namespace Stockify.Modules.Stocks.Domain.Portfolios;

public sealed class Portfolio : Entity<PortfolioId>
{
    private readonly List<AllocationEntry> _allocations = [];
    
    private Portfolio() : base(PortfolioId.New())
    {
    }

    public StockholderId StockholderId { get; private set; }
    public decimal RiskScore { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public IReadOnlyCollection<AllocationEntry> Allocations => [.. _allocations];
    
    public static Portfolio Create(
        StockholderId stockholderId,
        decimal riskScore,
        IEnumerable<AllocationEntry> allocations)
    {
        IEnumerable<AllocationEntry> entries = allocations.ToList();
        
        decimal total = entries.Sum(a => a.Percentage);
        
        if (Math.Abs(total - 100m) > 0.01m)
        {
            throw new ArgumentException("Total allocation must equal 100%");
        }

        var portfolio = new Portfolio
        {
            StockholderId = stockholderId,
            RiskScore = riskScore,
            CreatedAtUtc = DateTime.UtcNow
        };
        
        portfolio._allocations.AddRange(entries);
        
        return portfolio;
    }
}
