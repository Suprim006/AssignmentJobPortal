using AssignmentJobPortal.Entities;
using Microsoft.EntityFrameworkCore;
using AssignmentJobPortal.Models;

namespace AssignmentJobPortal
{
    public class AppDbContext : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

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
        public DbSet<AssignmentJobPortal.Models.UserViewModel> UserViewModel { get; set; } = default!;
        public DbSet<AssignmentJobPortal.Models.JobViewModel> JobViewModel { get; set; } = default!;
    }
}
