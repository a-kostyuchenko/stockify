using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Stocks.Domain.Stockholders;
using Stockify.Modules.Stocks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Stocks.Infrastructure.Database.Configurations;

internal sealed class StockholderConfiguration : IEntityTypeConfiguration<Stockholder>
{
    public void Configure(EntityTypeBuilder<Stockholder> builder)
    {
        builder.ToTable(TableNames.Stockholders);
        
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.Id)
            .HasConversion(stockholderId => stockholderId.Value, value => StockholderId.From(value));
        
        builder.Property(u => u.FirstName).HasMaxLength(200);

        builder.Property(u => u.LastName).HasMaxLength(200);

        builder.Property(u => u.Email).HasMaxLength(300);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
