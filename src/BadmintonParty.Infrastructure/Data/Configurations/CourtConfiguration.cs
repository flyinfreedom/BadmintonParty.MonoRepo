using BadmintonParty.Domain.AggregatesModel.CourtAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadmintonParty.Infrastructure.Data.Configurations;

public class CourtConfiguration : IEntityTypeConfiguration<Court>
{
    public void Configure(EntityTypeBuilder<Court> builder)
    {
        builder.ToTable("Courts");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Status)
            .HasConversion<int>();

        builder.Property(c => c.PublicId)
            .IsRequired();

        builder.HasIndex(c => c.PublicId).IsUnique();

        builder.HasOne<CourtHall>()
            .WithMany()
            .HasForeignKey(c => c.HallId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
