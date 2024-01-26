using AutoMapper;
using LaBenVi_AuthService.Models;
using LaBenVi_AuthService.Models.Dto;
using LaBenVi_AuthService.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace LaBenVi_AuthService.Service
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public UserManagementService(UserManager<AppUser> userManager, IRepository repository, IMapper mapper)
        {
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
        }




        public async Task<IEnumerable<AppUserDto>> GetAllUsers()
        {
            var users = (await _repository.GetAllAsync2<AppUser>())
            .Where(user => user.DeletedAt == null);

            var userDtoList = new List<AppUserDto>();

            foreach (var user in users)
            {
                var userRole = await _userManager.GetRolesAsync(user);

                var userDto = new AppUserDto
                {
                    ID = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    ImageUrl = user.ImageUrl,
                    RoleName = userRole is not null ? userRole : null
                };

                userDtoList.Add(userDto);
            }

            return userDtoList;
        }

        public async Task<AppUserDto> GetUserById(string userId)
        {
            var existingUser = await _repository.GetByIdAsync<AppUser>(userId);

            if (existingUser == null || existingUser.DeletedAt is not null)
                return null;

            var userDto = _mapper.Map<AppUserDto>(existingUser);

            return userDto;
        }

        public async Task<object> SoftDeleteUser(string Id)
        {
            var user = await _repository.GetByIdAsync<AppUser>(Id);

            if (user == null || !(user.DeletedAt is null))
                return false;

            user.DeletedAt = DateTime.UtcNow;
            user.UpdatedOn = DateTime.UtcNow;

            await _repository.UpdateAsync(user);
            return new { deletedAt = user.DeletedAt };
        }


        public async Task<AppUserUpdateRequestDto> UpdateUser(string id, AppUserUpdateRequestDto appUser)
        {
            var user = await _repository.GetByIdAsync<AppUser>(id);

            if (user == null || user.DeletedAt is not null)
                throw new Exception("User not found");

            user.Name = appUser.Name;
            user.Email = appUser.Email;
            user.PhoneNumber = appUser.PhoneNumber;
            user.ImageUrl = appUser.ImageUrl;
            user.UpdatedOn = DateTime.UtcNow;

            await _repository.UpdateAsync<AppUser>(user);

            var updatedUser = new AppUserUpdateRequestDto
            {
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                ImageUrl = user.ImageUrl,
            };

            return updatedUser;
        }
    }
}
