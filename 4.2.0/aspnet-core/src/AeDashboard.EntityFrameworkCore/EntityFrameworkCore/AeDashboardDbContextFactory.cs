using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using AeDashboard.Configuration;
using AeDashboard.Web;

namespace AeDashboard.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AeDashboardDbContextFactory : IDesignTimeDbContextFactory<AeDashboardDbContext>
    {
        public AeDashboardDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AeDashboardDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            AeDashboardDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AeDashboardConsts.ConnectionStringName));

            return new AeDashboardDbContext(builder.Options);
        }
    }
}
