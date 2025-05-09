using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShelfApi.Infrastructure.Models;

namespace ShelfApi.Infrastructure.Data;

public class DomainEventOutboxMessageConfiguration : IEntityTypeConfiguration<DomainEventOutboxMessage>
{
    public void Configure(EntityTypeBuilder<DomainEventOutboxMessage> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.EventType)
            .IsRequired();

        builder.Property(e => e.Payload)
            .IsRequired();

        builder.Property(e => e.OccurredOnUtc)
            .IsRequired();

        builder.Property(e => e.ProcessedOnUtc);

        builder.Property(e => e.RetryCount)
            .IsRequired();

        builder.Property(e => e.Error);
    }
}