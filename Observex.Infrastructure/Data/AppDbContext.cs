using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Observex.Core.Identity;
using Observex.Core.Entities;
using Observex.Infrastructure.Data.Seeders;

namespace Observex.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<SafetyOfficer> SafetyOfficers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Violation> Violations { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            // Seeding Departments
            DepartmentSeeder.SeedDepartments(modelBuilder);

            // Seeding Roles
            RoleSeeder.SeedRoles(modelBuilder);
        }
    }
}
