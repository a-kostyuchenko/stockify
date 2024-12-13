using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Stocks.Domain.Tickers;
using Stockify.Modules.Stocks.Domain.TickerTypes;
using Stockify.Modules.Stocks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Stocks.Infrastructure.Database.Configurations;

internal sealed class TickerConfiguration : IEntityTypeConfiguration<Ticker>
{
    public void Configure(EntityTypeBuilder<Ticker> builder)
    {
        builder.ToTable(TableNames.Tickers);
        
        builder.HasKey(t => t.Id);
        
        builder.Property(t => t.Id)
            .HasConversion(tickerId => tickerId.Value, value => TickerId.From(value));
        
        builder.Property(t => t.Symbol).HasMaxLength(5);
        
        builder.Property(t => t.Name).HasMaxLength(200);

        builder.Property(t => t.Description).HasMaxLength(500);
        
        builder.Property(t => t.Cik).HasMaxLength(10);

        builder.HasIndex(t => t.Symbol).IsUnique();
        
        builder.HasIndex(t => t.Cik).IsUnique();
        
        builder.HasOne<TickerType>()
            .WithMany()
            .HasForeignKey(t => t.TickerTypeId)
            .IsRequired();
    }
}
