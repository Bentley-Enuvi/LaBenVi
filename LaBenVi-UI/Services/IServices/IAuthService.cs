using LaBenVi_UI.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace LaBenVi_UI.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> SignUpAsync(RegRequestDto regRequestDto);
        Task<ResponseDto?> RoleAssignmentAsync(RegRequestDto roleReg);
        //Task<List<IdentityRole>> GetAllRoles();

        //Task<bool> SendConfirmationEmailAsync(AppUser user, string confirmEmailAddress);

        //Task<bool> SendPasswordResetEmailAsync(AppUser user, string resetPasswordAction);
        //Task<bool> SendConfirmationEmailAsync2(AppUser user, string confirmEmailAddress);

    }
}
