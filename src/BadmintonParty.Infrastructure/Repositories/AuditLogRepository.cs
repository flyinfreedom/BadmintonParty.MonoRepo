using BadmintonParty.Domain.AggregatesModel.AuditLogAggregate;
using BadmintonParty.Infrastructure.Data;

namespace BadmintonParty.Infrastructure.Repositories;

public class AuditLogRepository : IAuditLogRepository
{
    private readonly BadmintonPartyDbContext _context;

    public AuditLogRepository(BadmintonPartyDbContext context)
    {
        _context = context;
    }

    public async Task AddAuditLogAsync(AuditLog auditLog)
    {
        _context.AuditLogs.Add(auditLog);
        await _context.SaveChangesAsync();
    }
}
