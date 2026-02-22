namespace BadmintonParty.Domain.AggregatesModel.TenateAggregate;
public class TenateHall
{
    public long Id { get; set; }
    public long TenateId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string LineOfficalAccount { get; set; } = string.Empty;
    public HallStatus Status { get; set; }
    public Guid PublicId { get; set; }
}