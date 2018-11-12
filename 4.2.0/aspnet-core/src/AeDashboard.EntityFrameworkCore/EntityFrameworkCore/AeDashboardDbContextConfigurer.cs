using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AeDashboard.EntityFrameworkCore
{
    public static class AeDashboardDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AeDashboardDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AeDashboardDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
