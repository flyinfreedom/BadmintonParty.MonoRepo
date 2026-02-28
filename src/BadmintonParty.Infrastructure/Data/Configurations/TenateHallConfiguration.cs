using BadmintonParty.Domain.AggregatesModel.TenateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadmintonParty.Infrastructure.Data.Configurations;

public class TenateHallConfiguration : IEntityTypeConfiguration<TenateHall>
{
    public void Configure(EntityTypeBuilder<TenateHall> builder)
    {
        builder.ToTable("TenateHalls");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.Property(t => t.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(t => t.ContactPhone)
            .HasMaxLength(50);

        builder.Property(t => t.LineOfficalAccount)
            .HasMaxLength(100);

        builder.Property(t => t.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(t => t.PublicId)
            .IsRequired();

        builder.HasOne<Tenate>()
            .WithMany()
            .HasForeignKey(t => t.TenateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.PublicId).IsUnique();
    }
}
