using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Risks.Infrastructure.Database.Configurations;

internal sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable(
            TableNames.Questions,
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_Questions_Weight",
                    sql: "weight > 0");
            });

        builder.HasKey(q => q.Id);
        
        builder.Property(q => q.Id)
            .HasConversion(
                id => id.Value,
                value => QuestionId.From(value)
            );

        builder.Property(q => q.Content).HasMaxLength(500);
        
        builder.Property(q => q.Category)
            .HasConversion(
                category => category.Name,
                value => QuestionCategory.FromName(value)
            )
            .HasMaxLength(100);

        builder.HasMany(q => q.Answers)
            .WithOne()
            .HasForeignKey(a => a.QuestionId)
            .IsRequired();
    }
}
