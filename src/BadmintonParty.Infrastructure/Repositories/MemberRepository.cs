using BadmintonParty.Domain.AggregatesModel.MemberAggregate;
using BadmintonParty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BadmintonParty.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly BadmintonPartyDbContext _context;

    public MemberRepository(BadmintonPartyDbContext context)
    {
        _context = context;
    }

    public async Task<Member?> GetByLineUserIdAsync(string lineUserId)
    {
        return await _context.Members
            .FirstOrDefaultAsync(m => m.LineUserId == lineUserId);
    }

    public async Task<Member> AddAsync(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task UpdateAsync(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
    }
}
