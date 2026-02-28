using BadmintonParty.Domain.AggregatesModel.AuditLogAggregate;
using BadmintonParty.Domain.AggregatesModel.TenateAggregate;
using BadmintonParty.Infrastructure.Data;
using BadmintonParty.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BadmintonParty.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<BadmintonPartyDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<ITenateRepository, TenateRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();

        return services;
    }
}
