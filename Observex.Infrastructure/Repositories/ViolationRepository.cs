using Microsoft.EntityFrameworkCore;
using Observex.Core.Entities;
using Observex.Core.Interfaces;
using Observex.Infrastructure.Data;

namespace Observex.Infrastructure.Repositories
{
    public class ViolationRepository : Repository<Violation>, IViolationRepository
    {
        public ViolationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Violation>> GetViolationsByDateAsync(DateTime occurredDate)
        {
            return await _context.Violations.Where(v => v.OccurredDate == occurredDate).ToListAsync();
        }

        public async Task<IEnumerable<Violation>> GetWorkerViolationsByIdAsync(Guid workerId)
        {
            return await _context.Violations.Where(v => v.WorkerId == workerId).ToListAsync();
        }
    }
}
