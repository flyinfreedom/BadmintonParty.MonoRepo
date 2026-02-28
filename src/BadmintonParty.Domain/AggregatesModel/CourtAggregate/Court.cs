namespace BadmintonParty.Domain.AggregatesModel.CourtAggregate;
public class Court
{
    public long Id { get; set; }
    public long HallId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CourtStatus Status { get; set; }
    public Guid PublicId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long UpdatedBy { get; set; }
}