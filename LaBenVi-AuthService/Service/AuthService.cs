using LaBenVi_AuthService.Data;
using LaBenVi_AuthService.Models;
using LaBenVi_AuthService.Models.Dto;
using LaBenVi_AuthService.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace LaBenVi_AuthService.Service
{
    public class AuthService : IAuthService
    {
        private readonly LaBenViDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(LaBenViDbContext context,
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<string> SignUp(RegRequestDto regRequestDto)
        {
            AppUser user = new()
            {
                UserName = regRequestDto.Email,
                Email = regRequestDto.Email,
                NormalizedEmail = regRequestDto.Email.ToUpper(),
                Name = regRequestDto.Name,
                PhoneNumber = regRequestDto.PhoneNumber,
                Address = regRequestDto.Address,
                Password = regRequestDto.Password
            };

            try
            {
                var result = await _userManager.CreateAsync(user, regRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _context.AppUsers.First(u => u.UserName == regRequestDto.Email);

                    AppUserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                        Address = userToReturn.Address,
                        Password = userToReturn.Password
                    };

                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }



        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _context.AppUsers.FirstOrDefault(k => k.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            //Generate JWT Token if user was found
           
            var token = _jwtTokenGenerator.GenerateToken(user);

            AppUserDto userDTO = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Password = user.Password
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;
        }



        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _context.AppUsers.FirstOrDefault(g => g.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;

        }

        
        
    }
}
