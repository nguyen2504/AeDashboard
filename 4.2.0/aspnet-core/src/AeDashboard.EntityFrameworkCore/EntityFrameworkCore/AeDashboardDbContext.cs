using System.Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using AeDashboard.Authorization.Roles;
using AeDashboard.Authorization.Users;
using AeDashboard.Calendar;
using AeDashboard.Document;
using AeDashboard.MultiTenancy;

namespace AeDashboard.EntityFrameworkCore
{
    public class AeDashboardDbContext : AbpZeroDbContext<Tenant, Role, User, AeDashboardDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public AeDashboardDbContext(DbContextOptions<AeDashboardDbContext> options)
            : base(options)
        {
        }
        public DbSet<CalendarView> CalendarView { get; set; }
        //public DbSet<DocumentFile> DocumentFiles { get; set; }
    }
}
