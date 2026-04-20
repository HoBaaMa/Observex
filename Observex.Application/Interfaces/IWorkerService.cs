using Observex.Application.DTOs.Workers;
using Observex.Core.Entities;
using Observex.Core.Utils;

namespace Observex.Application.Interfaces
{
    public interface IWorkerService
    {
        Task<Result<WorkerDto>> GetByUserNameAsync(string username, CancellationToken cancellationToken = default);
        Task<Result<WorkerDto>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<WorkerDto>>> GetByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default);

        Task<Result<IEnumerable<WorkerDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Result<WorkerDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Result<WorkerDto>> CreateAsync(PostWorkerDto dto, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(Guid id, PostWorkerDto dto, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
