using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Common.Infrastructure.Data.Converters;
using Stockify.Modules.Stocks.Domain.Stockholders;
using Stockify.Modules.Stocks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Stocks.Infrastructure.Database.Configurations;

internal sealed class StockholderConfiguration : IEntityTypeConfiguration<Stockholder>
{
    public void Configure(EntityTypeBuilder<Stockholder> builder)
    {
        builder.ToTable(TableNames.Stockholders);
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .HasConversion(stockholderId => stockholderId.Value, value => StockholderId.From(value));
        
        builder.Property(s => s.FirstName).HasMaxLength(200);

        builder.Property(s => s.LastName).HasMaxLength(200);

        builder.Property(s => s.Email).HasMaxLength(300);

        builder.ComplexProperty(s => s.RiskProfile, profileBuilder =>
        {
            profileBuilder.Property(p => p.AttitudeType)
                .HasColumnName("attitude_type")
                .HasMaxLength(50)
                .HasConversion(new NullableStringConverter())
                .IsRequired(false);

            profileBuilder.Property(p => p.Coefficient)
                .HasColumnName("risk_coefficient")
                .HasPrecision(18, 4);

            profileBuilder.Property(p => p.UpdatedAtUtc)
                .HasColumnType("timestamp with time zone")
                .IsRequired(false);
        });

        builder.HasIndex(u => u.Email).IsUnique();
    }
}
