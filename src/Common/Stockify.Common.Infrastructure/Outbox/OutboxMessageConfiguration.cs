using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stockify.Common.Infrastructure.Outbox;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");
        
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Content)
            .HasMaxLength(2000)
            .HasColumnType("jsonb");

        builder.HasIndex(o => new { o.OccurredOnUtc, o.ProcessedOnUtc })
            .IncludeProperties(o => new { o.Id, o.Type, o.Content })
            .HasFilter("processed_on_utc IS NULL");
    }
}
