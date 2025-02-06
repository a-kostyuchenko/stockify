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
            .HasConversion(symbol => symbol.Value, value => Symbol.From(value))
            .HasColumnName("symbol")
            .HasMaxLength(20);
        
        builder.Property(t => t.Name).HasMaxLength(200);

        builder.Property(t => t.Description).HasMaxLength(500);
        
        builder.Property(t => t.Cik).HasMaxLength(10);
        
        builder.HasIndex(t => t.Cik).IsUnique();
        
        builder.HasIndex(t => new { t.Name, t.Description })
            .HasMethod("GIN")
            .IsTsVectorExpressionIndex("english");
        
        builder.HasOne<TickerType>()
            .WithMany()
            .HasForeignKey(t => t.TickerTypeId)
            .IsRequired();
    }
}
