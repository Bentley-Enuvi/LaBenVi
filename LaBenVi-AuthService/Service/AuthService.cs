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
        private readonly IMessengerService _messengerService;

        public AuthService(LaBenViDbContext context,
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator,
            IMessengerService messengerService)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _messengerService = messengerService;
        }


        public async Task<Result<AppUserDto>> SignUp(RegRequestDto regRequestDto)
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

            var result = await _userManager.CreateAsync(user, regRequestDto.Password);
            if (!result.Succeeded)
                return Result.Failure<AppUserDto>(result.Errors.Select(e => new Error(e.Code, e.Description)));

            var userToReturn = _context.AppUsers.First(u => u.UserName == regRequestDto.Email);
            var roles = await _userManager.GetRolesAsync(userToReturn);

            AppUserDto userDto = new()
            {
                Email = userToReturn.Email,
                ID = userToReturn.Id,
                Name = userToReturn.Name,
                PhoneNumber = userToReturn.PhoneNumber,
                Address = userToReturn.Address,
                Password = userToReturn.Password,
                ImageUrl = userToReturn.ImageUrl,
                RoleName = roles
            };

            return Result.Success(userDto);

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
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

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


        public async Task<bool> SendConfirmationEmailAsync(AppUser user, string confirmEmailAddress)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{confirmEmailAddress}?token={token}&email={user.Email}";
            var message = new EmailLogger("Confirmation email link", new List<string>() { user.Email },
                $"<a href=\"{confirmationLink}\">Click to confirm Confirmation email</a>");

            return await _messengerService.Send(message);
        }



        public async Task<bool> SendConfirmationEmailAsync2(AppUser user, string confirmEmailAddress)
        {
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = $"{confirmEmailAddress}?token={token}&email={user.Email}";

                var senderEmail = "labenvi@gmail.com";
                var message = new EmailLogger(
                    "Email Confirmation",
                    new List<string> { user.Email },
                    $"Dear {user.UserName},\n\nThank you for registering. Please confirm your email by clicking on the link: {confirmationLink}"
                );

                var sendResult = await _messengerService.Send(message);

                return sendResult;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return false;
            }
        }

        public async Task<bool> SendPasswordResetEmailAsync(AppUser user, string resetPasswordAddress)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = $"{resetPasswordAddress}?token={token}&email={user.Email}";
            var message = new EmailLogger("Reset Password link", new List<string>() { user.Email }, $"<a href=\"{link}\">Reset password</a>");

            return await _messengerService.Send(message);
        }

    }
}
