using BadmintonParty.Domain.AggregatesModel.AuditLogAggregate;
using BadmintonParty.Domain.AggregatesModel.CourtAggregate;
using BadmintonParty.Domain.AggregatesModel.MemberAggregate;
using Microsoft.EntityFrameworkCore;

namespace BadmintonParty.Infrastructure.Data;

public class BadmintonPartyDbContext : DbContext
{
    public BadmintonPartyDbContext(DbContextOptions<BadmintonPartyDbContext> options) : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();
    public DbSet<CourtHall> CourtHalls => Set<CourtHall>();
    public DbSet<Court> Courts => Set<Court>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BadmintonPartyDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
