namespace BadmintonParty.Domain.AggregatesModel.AuditLogAggregate;
public class AuditLog
{
    // 資料表名稱
    public string Domain { get; set; } = string.Empty;
    // 資料表主鍵值
    public string Key { get; set; } = string.Empty;
    // 操作類型
    public AuditType Action { get; set; }
    // 舊值
    public string? OldValue { get; set; }
    // 新值
    public string? NewValue { get; set; }
    // 操作人
    public long Operator { get; set; }
    // 操作時間    
    public DateTime OperateTime { get; set; }
    // 來源 BadmintonParty | CourtManagement
    public string Source { get; set; } = string.Empty;
}