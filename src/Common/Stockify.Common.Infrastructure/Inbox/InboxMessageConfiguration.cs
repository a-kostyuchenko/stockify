using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Stockify.Common.Infrastructure.Inbox;

internal sealed class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder.ToTable("inbox_messages");
        
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.Content)
            .HasMaxLength(2000)
            .HasColumnType("jsonb");
        
        builder.HasIndex(o => new { o.OccurredOnUtc, o.ProcessedOnUtc })
            .IncludeProperties(o => new { o.Id, o.Type, o.Content })
            .HasFilter("processed_on_utc IS NULL");
    }
}
