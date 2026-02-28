namespace BadmintonParty.Domain.AggregatesModel.AuditLogAggregate;

public interface IAuditLogRepository
{
    public Task AddAuditLogAsync(AuditLog auditLog);
}