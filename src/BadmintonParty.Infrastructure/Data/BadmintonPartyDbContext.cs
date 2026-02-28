using BadmintonParty.Domain.AggregatesModel.AuditLogAggregate;
using BadmintonParty.Domain.AggregatesModel.TenateAggregate;
using Microsoft.EntityFrameworkCore;

namespace BadmintonParty.Infrastructure.Data;

public class BadmintonPartyDbContext : DbContext
{
    public BadmintonPartyDbContext(DbContextOptions<BadmintonPartyDbContext> options) : base(options)
    {
    }

    public DbSet<Tenate> Tenates => Set<Tenate>();
    public DbSet<TenateAccount> TenateAccounts => Set<TenateAccount>();
    public DbSet<TenateHall> TenateHalls => Set<TenateHall>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BadmintonPartyDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
