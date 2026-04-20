using Microsoft.EntityFrameworkCore;
using Observex.Core.Entities;
using Observex.Core.Interfaces;
using Observex.Infrastructure.Data;

namespace Observex.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Department>> GetAllWithWorkersCountAsync(CancellationToken cancellationToken = default) => await _context.Departments
            .Include(w => w.Workers).ToListAsync(cancellationToken);

        public async Task<Department?> GetByIdWithWorkersCount(Guid id, CancellationToken cancellationToken = default) => await _context.Departments
            .AsNoTracking()
            .Include(w => w.Workers).FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }
}
