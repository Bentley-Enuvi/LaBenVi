using LaBenVi_AuthService.Models.Dto;
using System.Threading.Tasks;

namespace LaBenVi_AuthService.Service.IService
{
    public interface IUserManagementService
    {
        Task<IEnumerable<AppUserDto>> GetAllUsers();
        Task<AppUserDto> GetUserById(string userId);
        Task<object> SoftDeleteUser(string Id);
        Task<AppUserUpdateRequestDto> UpdateUser(string id, AppUserUpdateRequestDto appUser);
    }
}
