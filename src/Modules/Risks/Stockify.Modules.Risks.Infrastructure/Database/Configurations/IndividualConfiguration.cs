using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Risks.Infrastructure.Database.Configurations;

internal sealed class IndividualConfiguration : IEntityTypeConfiguration<Individual>
{
    public void Configure(EntityTypeBuilder<Individual> builder)
    {
        builder.ToTable(TableNames.Individuals);
        
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.Id)
            .HasConversion(individualId => individualId.Value, value => IndividualId.From(value));
        
        builder.Property(u => u.FirstName).HasMaxLength(200);

        builder.Property(u => u.LastName).HasMaxLength(200);

        builder.Property(u => u.Email).HasMaxLength(300);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.ComplexProperty(i => i.Attitude, attitudeBuilder =>
        {
            attitudeBuilder.Property(a => a.Coefficient)
                .HasColumnName("risk_coefficient")
                .HasPrecision(18, 4);
        });
    }
}
