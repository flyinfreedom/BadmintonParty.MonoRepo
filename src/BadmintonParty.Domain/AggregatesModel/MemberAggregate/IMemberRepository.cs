using BadmintonParty.Domain.AggregatesModel.MemberAggregate;

namespace BadmintonParty.Domain.AggregatesModel.MemberAggregate;

public interface IMemberRepository
{
    Task<Member?> GetByLineUserIdAsync(string lineUserId);
    Task<Member> AddAsync(Member member);
    Task UpdateAsync(Member member);
}
