namespace BadmintonParty.Domain.AggregatesModel.TenateAggregate;
public class Tenate
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TenateStatus Status { get; set; }
    public Guid PublicId { get; set; }
}