using Microsoft.EntityFrameworkCore;
using Observex.Core.Entities;
using Observex.Core.Interfaces;
using Observex.Infrastructure.Data;

namespace Observex.Infrastructure.Repositories
{
    public class WorkerRepository : Repository<Worker>, IWorkerRepository
    {
        public WorkerRepository(AppDbContext context) : base(context)
        {
        }
        
        public async Task<Worker?> GetByNameAsync(string name, CancellationToken cancellationToken = default) => 
            await _context.Workers.FirstOrDefaultAsync(w => w.FullName.Contains(name), cancellationToken);

        public async Task<Worker?> GetByUserNameAsync(string username, CancellationToken cancellationToken = default) => 
            await _context.Workers.FirstOrDefaultAsync(w => w.DisplayUserName == username, cancellationToken);
    }
}