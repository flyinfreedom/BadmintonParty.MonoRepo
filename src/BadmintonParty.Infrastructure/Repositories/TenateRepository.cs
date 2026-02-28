using BadmintonParty.Domain.AggregatesModel.TenateAggregate;
using BadmintonParty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BadmintonParty.Infrastructure.Repositories;

public class TenateRepository : ITenateRepository
{
    private readonly BadmintonPartyDbContext _context;

    public TenateRepository(BadmintonPartyDbContext context)
    {
        _context = context;
    }

    public async Task<Tenate?> GetTenateByIdAsync(long id)
    {
        return await _context.Tenates.FindAsync(id);
    }

    public async Task<Tenate?> GetTenateByPublicIdAsync(Guid publicId)
    {
        return await _context.Tenates.FirstOrDefaultAsync(t => t.PublicId == publicId);
    }

    public async Task<Tenate> CreateTenateAsync(Tenate tenate)
    {
        _context.Tenates.Add(tenate);
        await _context.SaveChangesAsync();
        return tenate;
    }

    public async Task UpdateTenateAsync(Tenate tenate)
    {
        _context.Tenates.Update(tenate);
        await _context.SaveChangesAsync();
    }

    public async Task CloseTenateAsync(Tenate tenate)
    {
        tenate.Status = TenateStatus.Deleted; 
        _context.Tenates.Update(tenate);
        await _context.SaveChangesAsync();
    }

    public async Task<TenateAccount?> GetTenateAccountAsync(string account)
    {
        return await _context.TenateAccounts.FirstOrDefaultAsync(a => a.Account == account);
    }

    public async Task<TenateAccount> CreateTenateAccountAsync(TenateAccount tenateAccount)
    {
        _context.TenateAccounts.Add(tenateAccount);
        await _context.SaveChangesAsync();
        return tenateAccount;
    }

    public async Task UpdateTenateAccountAsync(TenateAccount tenateAccount)
    {
        _context.TenateAccounts.Update(tenateAccount);
        await _context.SaveChangesAsync();
    }

    public async Task<TenateHall?> GetTenateHallByIdAsync(long id)
    {
        return await _context.TenateHalls.FindAsync(id);
    }

    public async Task<TenateHall> CreateTenateHallAsync(TenateHall tenateHall)
    {
        _context.TenateHalls.Add(tenateHall);
        await _context.SaveChangesAsync();
        return tenateHall;
    }

    public async Task UpdateTenateHallAsync(TenateHall tenateHall)
    {
        _context.TenateHalls.Update(tenateHall);
        await _context.SaveChangesAsync();
    }
}
