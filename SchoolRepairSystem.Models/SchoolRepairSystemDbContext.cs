using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SchoolRepairSystem.Models
{
    public class SchoolRepairSystemDbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => {
                builder.AddFilter((category, level) =>
                    category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information
                ).AddConsole();
            });

        public SchoolRepairSystemDbContext()
        {
            
        }
        public SchoolRepairSystemDbContext(DbContextOptions<SchoolRepairSystemDbContext> options) :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(MyLoggerFactory).UseSqlite("data source=NetCore_sqlite.db", b => b.MigrationsAssembly("SchoolRepairSystem.Api"));
           
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<ReportForRepair> ReportForRepair { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<UserWareHouse> UserWareHouse { get; set; }
        public DbSet<Menus> Menus { get; set; }
    }
}