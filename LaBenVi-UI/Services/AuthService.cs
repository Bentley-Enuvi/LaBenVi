using LaBenVi_UI.Models;
using LaBenVi_UI.Services.IServices;
using LaBenVi_UI.Utilities;
using Microsoft.AspNetCore.Identity.Data;

namespace LaBenVi_UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }



        public async Task<ResponseDto?> SignUpAsync(RegRequestDto regRequestDto)
        {
            const string url = "/account/register";
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.POST,
                Data = regRequestDto,
                Url = Static_Details.AuthAPIBase + "/account/register",
            });
        }




        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.POST,
                Data = loginRequestDto,
                Url = Static_Details.AuthAPIBase + "/auth/login"
            });
        }


        public async Task<ResponseDto?> RoleAssignmentAsync(RegRequestDto roleReg)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.POST,
                Data = roleReg,
                Url = Static_Details.AuthAPIBase + "/auth/AssignRole"
            });
        }
    }
}
