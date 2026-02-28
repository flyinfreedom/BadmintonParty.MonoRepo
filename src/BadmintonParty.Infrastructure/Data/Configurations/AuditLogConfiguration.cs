using BadmintonParty.Domain.AggregatesModel.AuditLogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadmintonParty.Infrastructure.Data.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        // Use a shadow property for the Id since it's not in the domain model
        builder.Property<long>("Id").ValueGeneratedOnAdd();
        builder.HasKey("Id");

        builder.Property(t => t.Domain)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Key)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Action)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(t => t.Source)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.OldValue);
        builder.Property(t => t.NewValue);
    }
}
