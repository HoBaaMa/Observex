using Microsoft.EntityFrameworkCore;
using Observex.Core.Entities;
using Observex.Core.Interfaces;
using Observex.Infrastructure.Data;

namespace Observex.Infrastructure.Repositories
{
    public class SafetyOfficerRepository : Repository<SafetyOfficer>, ISafetyOfficerRepository
    {
        public SafetyOfficerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<SafetyOfficer?> GetByNameAsync(string name) => await _context.SafetyOfficers.FirstOrDefaultAsync(so => so.FullName.Contains(name));

        public async Task<SafetyOfficer?> GetByUserNameAsync(string username) => await _context.SafetyOfficers.FirstOrDefaultAsync(so => so.DisplayUserName == username);
    }
}
