using Observex.Application.DTOs.Identity;
using Observex.Core.Identity;

namespace Observex.Application.Interfaces
{
    public interface IJwtService
    {
        AuthenticationResponseDto GenerateJwtToken(ApplicationUser applicationUser);
    }
}
