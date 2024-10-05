using Health_Guard_Assistant.services.AuthService.Models;

namespace Health_Guard_Assistant.services.AuthService.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
