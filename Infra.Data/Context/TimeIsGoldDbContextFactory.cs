using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.Data
{
    public class TimeIsGoldDbContextFactory : IDesignTimeDbContextFactory<TimeIsGoldDbContext>
    {
        public TimeIsGoldDbContext CreateDbContext(string[] args)
        {
            // Caminho para o appsettings.json
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "TimeIsGold");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<TimeIsGoldDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new TimeIsGoldDbContext(optionsBuilder.Options);
        }
    }
}