using LaBenVi_UI.Models;
using LaBenVi_UI.Services;
using LaBenVi_UI.Services.IServices;
using LaBenVi_UI.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;

namespace LaBenVi_UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        //private readonly RoleManager<AppUser> _roleManager;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text = Static_Details.RoleAdmin, Value = Static_Details.RoleAdmin },
                new SelectListItem{Text = Static_Details.RoleEditor, Value = Static_Details.RoleEditor },
                new SelectListItem{Text = Static_Details.RoleRegular, Value = Static_Details.RoleRegular },
            };

            ViewBag.RoleList = roleList;

            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(RegRequestDto model)
        {
            ResponseDto response = await _authService.SignUpAsync(model);
            ResponseDto assignRole;

            if (response != null && response.IsSuccess)
            {

                if (string.IsNullOrEmpty(model.Role))
                {
                    model.Role = Static_Details.RoleRegular;
                }
                assignRole = await _authService.RoleAssignmentAsync(model);

                if (assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration successful";

                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = response.Message;
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text = Static_Details.RoleAdmin, Value = Static_Details.RoleAdmin },
                new SelectListItem{Text = Static_Details.RoleEditor, Value = Static_Details.RoleEditor },
                new SelectListItem{Text = Static_Details.RoleRegular, Value = Static_Details.RoleRegular },
            };

            ViewBag.RoleList = roleList;

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            ResponseDto loginResponse = await _authService.LoginAsync(model);

            if (loginResponse != null && loginResponse.IsSuccess)
            {

                LoginResponseDto loginResponseDto =
                    JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(loginResponse.Result));

                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = loginResponse.Message;
                return View(model);
            }

        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }


        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            if (jwt.Claims != null)
            {
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                    jwt.Claims.FirstOrDefault(m => m.Type == JwtRegisteredClaimNames.Email)?.Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                    jwt.Claims.FirstOrDefault(m => m.Type == JwtRegisteredClaimNames.Sub)?.Value));
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                    jwt.Claims.FirstOrDefault(m => m.Type == JwtRegisteredClaimNames.Name)?.Value));

                // Another example of a null check
                var roleClaim = jwt.Claims.FirstOrDefault(m => m.Type == "role");
                if (roleClaim != null)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
                }

                identity.AddClaim(new Claim(ClaimTypes.Name,
                    jwt.Claims.FirstOrDefault(m => m.Type == JwtRegisteredClaimNames.Email)?.Value));
                //identity.AddClaim(new Claim(ClaimTypes.Role,
                //    jwt.Claims.FirstOrDefault(m => m.Type == "role").Value));

            }


            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

    }
}
