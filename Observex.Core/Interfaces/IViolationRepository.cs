using Observex.Core.Entities;

namespace Observex.Core.Interfaces
{
    public interface IViolationRepository : IRepository<Violation>
    {
        Task <IEnumerable<Violation>> GetWorkerViolationsByIdAsync(Guid workerId);
        Task<IEnumerable<Violation>> GetViolationsByDateAsync(DateTime occurredDate);

        // Much More Will Be Added...
    }
}
