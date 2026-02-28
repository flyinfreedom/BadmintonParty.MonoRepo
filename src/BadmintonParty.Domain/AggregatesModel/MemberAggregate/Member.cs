namespace BadmintonParty.Domain.AggregatesModel.MemberAggregate;
public class Member
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LineUserId { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
    public List<MemberRecentOpening> RecentOpenings { get; set; } = new();
    public AccountStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long UpdatedBy { get; set; }
}