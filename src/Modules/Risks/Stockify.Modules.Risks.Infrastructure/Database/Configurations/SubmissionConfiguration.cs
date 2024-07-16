using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;
using Stockify.Modules.Risks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Risks.Infrastructure.Database.Configurations;

internal sealed class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.ToTable(
            TableNames.Submissions,
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint(
                    "CK_Points_NotNegative",
                    sql: "points >= 0");
            });

        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => SubmissionId.FromValue(value)
            );

        builder.HasOne<Question>()
            .WithMany()
            .HasForeignKey(s => s.QuestionId)
            .IsRequired();

        builder.HasOne<Answer>()
            .WithMany()
            .HasForeignKey(s => s.AnswerId)
            .IsRequired();
    }
}
