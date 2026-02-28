using BadmintonParty.Domain.AggregatesModel.TenateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BadmintonParty.Infrastructure.Data.Configurations;

public class TenateAccountConfiguration : IEntityTypeConfiguration<TenateAccount>
{
    public void Configure(EntityTypeBuilder<TenateAccount> builder)
    {
        builder.ToTable("TenateAccounts");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Account)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Password)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne<Tenate>()
            .WithMany()
            .HasForeignKey(t => t.TenateId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.Account).IsUnique();
    }
}
