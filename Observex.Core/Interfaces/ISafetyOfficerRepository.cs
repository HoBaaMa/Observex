using Observex.Core.Entities;

namespace Observex.Core.Interfaces
{
    public interface ISafetyOfficerRepository : IRepository<SafetyOfficer>
    {
        Task<SafetyOfficer?> GetByUserNameAsync(string username);
        Task<SafetyOfficer?> GetByNameAsync(string name);
    }
}
