using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Stocks.Domain.Stockholders;
using Stockify.Modules.Stocks.Domain.Subscriptions;
using Stockify.Modules.Stocks.Domain.Tickers;
using Stockify.Modules.Stocks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Stocks.Infrastructure.Database.Configurations;

internal sealed class TickerSubscriptionConfiguration : IEntityTypeConfiguration<TickerSubscription>
{
    public void Configure(EntityTypeBuilder<TickerSubscription> builder)
    {
        builder.ToTable(TableNames.TickerSubscriptions);
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
            .HasConversion(subscriptionId => subscriptionId.Value, value => TickerSubscriptionId.From(value));
        
        builder.Property(t => t.Symbol)
            .HasConversion(symbol => symbol.Value, value => Symbol.From(value));
        
        builder.Property(t => t.StockholderId)
            .HasConversion(stockholderId => stockholderId.Value, value => StockholderId.From(value));
        
        builder.HasIndex(t => new { t.Symbol, t.StockholderId })
            .HasFilter("active = true")
            .IsUnique();

        builder.HasOne<Ticker>()
            .WithMany()
            .HasForeignKey(t => t.Symbol)
            .IsRequired();

        builder.HasOne<Stockholder>()
            .WithMany()
            .HasForeignKey(t => t.StockholderId)
            .IsRequired();
    }
}
