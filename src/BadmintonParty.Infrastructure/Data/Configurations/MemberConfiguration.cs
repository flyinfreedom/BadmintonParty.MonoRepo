using BadmintonParty.Domain.AggregatesModel.MemberAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadmintonParty.Infrastructure.Data.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.LineUserId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.PictureUrl)
            .HasMaxLength(500);

        builder.Property(m => m.Status)
            .HasConversion<int>();

        builder.HasIndex(m => m.LineUserId).IsUnique();

        // Separate table for RecentOpenings or JSON? 
        // For simplicity with PostgreSQL, let's use JSONB or a separate table.
        // Given MemberRecentOpening doesn't have an Id, it's likely an Owned Entity.
        builder.OwnsMany(m => m.RecentOpenings, a =>
        {
            a.ToTable("MemberRecentOpenings");
            a.WithOwner().HasForeignKey("MemberId");
            a.Property(x => x.CourtId).HasMaxLength(100);
            a.Property(x => x.CourtName).HasMaxLength(200);
            a.Property(x => x.Location).HasMaxLength(500);
        });
    }
}
