namespace BadmintonParty.Domain.AggregatesModel.MemberAggregate;
public class MemberRecentOpening
{ 
    public string CourtId { get; set; }
    public string CourtName { get; set; }
    public string Location {  get; set; }
    public DateTime OpeningTime {  get; set; }
}