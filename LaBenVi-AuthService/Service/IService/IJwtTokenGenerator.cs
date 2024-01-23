using LaBenVi_AuthService.Models;

namespace LaBenVi_AuthService.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(AppUser appUser, IEnumerable<string> roles);
    }
}
