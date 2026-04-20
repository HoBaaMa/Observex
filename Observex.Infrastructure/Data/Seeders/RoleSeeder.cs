using Microsoft.EntityFrameworkCore;
using Observex.Core.Identity;

namespace Observex.Infrastructure.Data.Seeders
{
    internal class RoleSeeder
    {
        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            var roles = new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Id = Guid.Parse("089B76D0-BF3B-455A-9604-A8C287E17DB2"),
                    Name = "Worker",
                    NormalizedName = "WORKER",
                    ConcurrencyStamp = "76C6AEBB-A8BB-4DE2-B7BE-5422D77EB7DE"
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("BC3B846F-2562-4493-8959-B663A7804517"),
                    Name = "Safety Officer",
                    NormalizedName = "SAFETY OFFICER",
                    ConcurrencyStamp = "821E75EF-EC7F-446F-AB73-3AB7EC2B5DC2"
                }
            };

            modelBuilder.Entity<ApplicationRole>().HasData(roles);
        }
    }
}
