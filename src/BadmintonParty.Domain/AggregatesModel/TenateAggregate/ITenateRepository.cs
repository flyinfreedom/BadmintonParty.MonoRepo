namespace BadmintonParty.Domain.AggregatesModel.TenateAggregate;
public interface ITenateRepository
{
    Task<Tenate?> GetTenateByIdAsync(long id);
    Task<Tenate?> GetTenateByPublicIdAsync(Guid publicId);
    Task<Tenate> CreateTenateAsync(Tenate tenate);
    Task UpdateTenateAsync(Tenate tenate);
    Task CloseTenateAsync(Tenate tenate);

    Task<TenateAccount?> GetTenateAccountAsync(string account);
    Task<TenateAccount> CreateTenateAccountAsync(TenateAccount tenateAccount);
    Task UpdateTenateAccountAsync(TenateAccount tenateAccount);

    Task<TenateHall?> GetTenateHallByIdAsync(long id);
    Task<TenateHall> CreateTenateHallAsync(TenateHall tenateHall);
    Task UpdateTenateHallAsync(TenateHall tenateHall);
}