using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Risks.Infrastructure.Database.Configurations;

internal sealed class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable(
            TableNames.Answers,
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_Points_NotNegative",
                    sql: "points >= 0");
            });

        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => AnswerId.FromValue(value)
            );

        builder.Property(a => a.Content).HasMaxLength(500);
    }
}
