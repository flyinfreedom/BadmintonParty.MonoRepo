using BadmintonParty.Domain.AggregatesModel.TenateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadmintonParty.Infrastructure.Data.Configurations;

public class TenateConfiguration : IEntityTypeConfiguration<Tenate>
{
    public void Configure(EntityTypeBuilder<Tenate> builder)
    {
        builder.ToTable("Tenates");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(t => t.PublicId)
            .IsRequired();

        builder.HasIndex(t => t.PublicId).IsUnique();
    }
}
