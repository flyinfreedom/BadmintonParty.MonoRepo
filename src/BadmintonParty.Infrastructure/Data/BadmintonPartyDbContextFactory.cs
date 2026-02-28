using BadmintonParty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BadmintonParty.Infrastructure.Data;

public class BadmintonPartyDbContextFactory : IDesignTimeDbContextFactory<BadmintonPartyDbContext>
{
    public BadmintonPartyDbContext CreateDbContext(string[] args)
    {
        // 修正路徑邏輯，直接指向 Api 專案目錄
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "src", "BadmintonParty.Api");
        
        // 如果是在解決方案根目錄執行，檢查目錄是否存在
        if (!Directory.Exists(basePath))
        {
            // 如果是在 Infrastructure 目錄執行 (常見於某些 IDE 操作)
            basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, "BadmintonParty.Api");
        }
        
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<BadmintonPartyDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(connectionString);

        return new BadmintonPartyDbContext(builder.Options);
    }
}
