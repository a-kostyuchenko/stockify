using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Domain.Sessions;
using Stockify.Modules.Risks.Infrastructure.Database.Constants;

namespace Stockify.Modules.Risks.Infrastructure.Database.Configurations;

internal sealed class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable(TableNames.Sessions);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                sessionId => sessionId.Value,
                value => SessionId.From(value));

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion(status => status.Name, name => SessionStatus.FromName(name));

        builder.HasMany(s => s.Questions)
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("session_questions");
            });

        builder.HasOne<Individual>()
            .WithMany()
            .HasForeignKey(s => s.IndividualId)
            .IsRequired();

        builder.OwnsMany(s => s.Submissions, submissionBuilder =>
        {
            submissionBuilder.WithOwner()
                .HasForeignKey(s => s.SessionId);
            
            submissionBuilder.ToTable(
                TableNames.Submissions,
                tableBuilder =>
                {
                    tableBuilder.HasCheckConstraint(
                        "CK_Points_NotNegative",
                        sql: "points >= 0");
                });

            submissionBuilder.HasKey(s => s.Id);
        
            submissionBuilder.Property(s => s.Id)
                .HasConversion(
                    id => id.Value,
                    value => SubmissionId.From(value)
                );

            submissionBuilder.HasOne<Question>()
                .WithMany()
                .HasForeignKey(s => s.QuestionId)
                .IsRequired();

            submissionBuilder.HasOne<Answer>()
                .WithMany()
                .HasForeignKey(s => s.AnswerId)
                .IsRequired();
        });
    }
}
