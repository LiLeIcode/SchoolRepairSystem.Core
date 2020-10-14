using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SchoolRepairSystemModels
{
    public class SchoolRepairSystemDbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => {
                builder.AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information
                ).AddConsole();
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)=>
            optionsBuilder.UseLoggerFactory(MyLoggerFactory).
                UseSqlServer(@"Data Source=.;database=SchoolRepairSystem;Integrated Security=True;uid=sa;pwd=root");

        //public SchoolRepairSystemDbContext(DbContextOptions<SchoolRepairSystemDbContext> options) : base(options)
        //{
        //}

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ReportForRepair> ReportForRepairs { get; set; }
        public DbSet<RoleReportForRepair> RoleReportForRepairs { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<UserWareHouse> UserWareHouses { get; set; }
    }
}