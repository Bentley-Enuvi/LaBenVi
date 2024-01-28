
using LaBenVi_AuthService.Models;
using LaBenVi_AuthService.Models.Dto;

namespace LaBenVi_AuthService.Service.IService
{
    public interface IAuthService
    {
        Task<Result<AppUserDto>> SignUp(RegRequestDto regRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        Task<bool> SendConfirmationEmailAsync(AppUser user, string confirmEmailAddress);

        Task<bool> SendPasswordResetEmailAsync(AppUser user, string resetPasswordAction);
        Task<bool> SendConfirmationEmailAsync2(AppUser user, string confirmEmailAddress);

    }
}
