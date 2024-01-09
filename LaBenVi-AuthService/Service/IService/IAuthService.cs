
using LaBenVi_AuthService.Models.Dto;

namespace LaBenVi_AuthService.Service.IService
{
    public interface IAuthService
    {
        Task<string> SignUp(RegRequestDto regRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
