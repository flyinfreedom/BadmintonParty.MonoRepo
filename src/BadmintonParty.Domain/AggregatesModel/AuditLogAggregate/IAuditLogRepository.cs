namespace BadmintonParty.Domain.AggregatesModel.AuditLogAggregate;

public interface IAuditLogRepository
{
    public Task CreateAuditLogAsync(AuditLog auditLog);
}