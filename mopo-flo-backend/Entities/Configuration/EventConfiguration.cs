using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace violet.backend.Entities.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(x => x.AggregateType).IsRequired().IsUnicode(false).HasMaxLength(100);
        builder.Property(x => x.DomainEventType).IsRequired().IsUnicode(false).HasMaxLength(100);
        builder.Property(x => x.EventData).IsRequired().IsUnicode();
        builder.Property(x => x.Version).IsRequired().HasDefaultValue(1);
    }
}