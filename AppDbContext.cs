using AssignmentJobPortal.Entities;
using Microsoft.EntityFrameworkCore;
using AssignmentJobPortal.Models;

namespace AssignmentJobPortal
{
    public class AppDbContext : DbContext
    {
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<JobApplications> JobApplications { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }

        public string DbPath { get; } // Path to the SQLite DB file

        public AppDbContext()
        {
            var path = "C:\\Change_folder_after_HDD\\Class\\4th Sem\\Application Development\\codes\\Assignment\\AssignmentProjectFinal\\AssignmentJobPortal";
            DbPath = System.IO.Path.Join(path, "WebAppDb.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
