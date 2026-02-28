namespace BadmintonParty.Domain.AggregatesModel.CourtAggregate;
public interface ICourtRepository
{
    Task<Court?> GetCourtByIdAsync(long id);
    Task<Court?> GetCourtByPublicIdAsync(Guid publicId);
    Task<Court> CreateCourtAsync(Court court);
    Task UpdateCourtAsync(Court court);
    Task CloseCourtAsync(Court court);

    Task<CourtHall?> GetCourtHallByIdAsync(long id);
    Task<CourtHall?> GetCourtHallByPublicIdAsync(Guid publicId);
    Task<CourtHall> CreateCourtHallAsync(CourtHall courtHall);
    Task UpdateCourtHallAsync(CourtHall courtHall);
}