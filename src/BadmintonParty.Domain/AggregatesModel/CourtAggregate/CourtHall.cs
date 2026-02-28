namespace BadmintonParty.Domain.AggregatesModel.CourtAggregate;
public class CourtHall
{
    public long Id { get; set; }
    public long? MemberId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Address { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
    public string LineOfficalAccount { get; set; } = string.Empty;
    public HallStatus Status { get; set; }
    public Guid PublicId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long UpdatedBy { get; set; }
}