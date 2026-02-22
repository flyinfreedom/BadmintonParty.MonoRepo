namespace BadmintonParty.Domain.AggregatesModel.TenateAggregate;
public class TenateAccount
{
    public long Id { get; set; }
    public long TenateId { get; set; }
    public string Account { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public AccountStatus Status { get; set; }

}