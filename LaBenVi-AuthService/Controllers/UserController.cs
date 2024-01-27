using AutoMapper;
using LaBenVi_AuthService.Data;
using LaBenVi_AuthService.Models;
using LaBenVi_AuthService.Models.Dto;
using LaBenVi_AuthService.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LaBenVi_AuthService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly LaBenViDbContext _context;

        public UserController(
            IUserManagementService userService,
            SignInManager<AppUser> signInManager, IMapper mapper, LaBenViDbContext context)
        {
            _userService = userService;
            _signInManager = signInManager;
            _response = new();
            _mapper = mapper;
            _context = context;
        }

        // [Authorize(Roles = "ADMIN")]
        [HttpGet("get-all-users")]
        public ResponseDto GetAllUsers()
        {
            try
            {
                IEnumerable<AppUser> result = _context.AppUsers.ToList();
                _response.Result = _mapper.Map<IEnumerable<AppUserDto>>(result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //[Authorize(Roles = "ADMIN")]
        [HttpGet("get-user-by-id/{userId}")]
        public ResponseDto GetUserById(string userId)
        {
            try
            {
                AppUser result = _context.AppUsers.First(p => p.Id == userId);

                _response.Result = _mapper.Map<AppUserDto>(result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        

        // [Authorize(Roles = "ADMIN")]
        [HttpDelete("delete/{id}")]
        public ResponseDto DeleteUser(string userId)
        {

            try
            {
                AppUser result = _context.AppUsers.First(x => x.Id == userId);
                _context.AppUsers.Remove(result);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        // [Authorize(Roles = "ADMIN")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(
            string id,
            [FromBody] AppUserUpdateRequestDto appUserDTO)
        {
            var result = await _userService.UpdateUser(id, appUserDTO);

            if (result != null)
            {
                var response = new ResponseDto
                {
                    Code = 200,
                    Result = result,
                    Message = "User Updated Successfully",
                    Error = string.Empty,
                    IsSuccess = true
                };

                return Ok(response);
            }
            else
            {
                var response = new ResponseDto
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Result = null,
                    Message = "Failed to update User",
                    Error = "Failed"
                };

                return BadRequest(response);
            }
        }
    }
}
