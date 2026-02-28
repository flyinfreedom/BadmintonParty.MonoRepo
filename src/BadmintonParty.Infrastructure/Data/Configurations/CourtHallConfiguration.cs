using BadmintonParty.Domain.AggregatesModel.CourtAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadmintonParty.Infrastructure.Data.Configurations;

public class CourtHallConfiguration : IEntityTypeConfiguration<CourtHall>
{
    public void Configure(EntityTypeBuilder<CourtHall> builder)
    {
        builder.ToTable("CourtHalls");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(h => h.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(h => h.Status)
            .HasConversion<int>();

        builder.Property(h => h.PublicId)
            .IsRequired();

        builder.HasIndex(h => h.PublicId).IsUnique();
    }
}
