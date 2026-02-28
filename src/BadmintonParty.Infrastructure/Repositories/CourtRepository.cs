using BadmintonParty.Domain.AggregatesModel.CourtAggregate;
using BadmintonParty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BadmintonParty.Infrastructure.Repositories;

public class CourtRepository : ICourtRepository
{
    private readonly BadmintonPartyDbContext _context;

    public CourtRepository(BadmintonPartyDbContext context)
    {
        _context = context;
    }

    public async Task<Court?> GetCourtByIdAsync(long id)
    {
        return await _context.Courts.FindAsync(id);
    }

    public async Task<Court?> GetCourtByPublicIdAsync(Guid publicId)
    {
        return await _context.Courts.FirstOrDefaultAsync(c => c.PublicId == publicId);
    }

    public async Task<Court> CreateCourtAsync(Court court)
    {
        _context.Courts.Add(court);
        await _context.SaveChangesAsync();
        return court;
    }

    public async Task UpdateCourtAsync(Court court)
    {
        _context.Courts.Update(court);
        await _context.SaveChangesAsync();
    }

    public async Task CloseCourtAsync(Court court)
    {
        court.Status = CourtStatus.Deleted;
        _context.Courts.Update(court);
        await _context.SaveChangesAsync();
    }

    public async Task<CourtHall?> GetCourtHallByIdAsync(long id)
    {
        return await _context.CourtHalls.FindAsync(id);
    }

    public async Task<CourtHall?> GetCourtHallByPublicIdAsync(Guid publicId)
    {
        return await _context.CourtHalls.FirstOrDefaultAsync(h => h.PublicId == publicId);
    }

    public async Task<CourtHall> CreateCourtHallAsync(CourtHall courtHall)
    {
        _context.CourtHalls.Add(courtHall);
        await _context.SaveChangesAsync();
        return courtHall;
    }

    public async Task UpdateCourtHallAsync(CourtHall courtHall)
    {
        _context.CourtHalls.Update(courtHall);
        await _context.SaveChangesAsync();
    }
}
