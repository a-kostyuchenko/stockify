using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Stocks.Domain.TickerTypes;
using Stockify.Modules.Stocks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Stocks.Infrastructure.Database.Configurations;

internal sealed class TickerTypeConfiguration : IEntityTypeConfiguration<TickerType>
{
    public void Configure(EntityTypeBuilder<TickerType> builder)
    {
        builder.ToTable(TableNames.TickerTypes);
        
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.Id)
            .HasConversion(tickerTypeId => tickerTypeId.Value, value => TickerTypeId.From(value));
        
        builder.Property(u => u.Code).HasMaxLength(50);

        builder.Property(u => u.Description).HasMaxLength(200);
        
        builder.HasIndex(u => u.Code).IsUnique();
    }
}
