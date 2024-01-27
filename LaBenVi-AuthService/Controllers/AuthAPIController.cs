using LaBenVi_AuthService.Models;
using LaBenVi_AuthService.Models.Dto;
using LaBenVi_AuthService.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace LaBenVi_AuthService.Controllers
{
    [Route("api/auth")]
    public class AuthAPIController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IMessengerService _messengerService;
        private readonly IConfiguration _configuration;
        protected ResponseDto _response;
        private readonly UserManager<AppUser> _userManager;
        protected JsonConfirmDto _jsonConfirm;

        public AuthAPIController(IAuthService authService, 
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            IMessengerService messengerService)
        {
            _authService = authService;
            _configuration = configuration;
            _jsonConfirm = new();
            _response = new();
            _userManager = userManager;
            _messengerService = messengerService;
        }



        [HttpPost("register")]
        public async Task<IActionResult> SignUp([FromBody] RegRequestDto model)
        {
            var errorMessage = await _authService.SignUp(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                if (errorMessage.Contains("already taken"))
                {
                    // User already exists, consider it a success
                    _response.IsSuccess = true;
                    return Ok(_response);
                }

                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            //await _messengerService.Send(model.Email, _configuration.GetValue<string>("BodyNames:RegisterUserBody"));
            _response.IsSuccess = true;
            return Ok(_response);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);

        }



        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            // if email or token is null, end the process
            if (email == null || token == null)
            {
                return BadRequest(new
                { errorTitle = "Invalid Email or Token", errorMessage = "Email or token cannot be null" });
            }

            // ensure that user exists
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new { errorMessage = $"User with email {email} not found" });
            }

            // confirm email
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new { message = "Email confirmed successfully" });
            }

            // on failure
            return BadRequest(new { errorTitle = "Confirmation Failed", errorMessage = "Could not confirm email" });
        }


        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(xx => xx.ErrorMessage));
                var error = string.Join(" ", errors);
                return BadRequest(new { error = error });

                //JsonConfirmDto jsonConfirm = new();
                //var user = await _userManager.FindByEmailAsync(model.Email);
                //if (user == null)
                //{
                //    _jsonConfirm.AlertMessage = "User not found with this email: " + model.Email;
                //    _jsonConfirm.IsSuccess = false;
                //    _jsonConfirm.EmailSent = DateTime.Now;
                //    return new JsonResult(_jsonConfirm);
                //}
                //else if (!(await _userManager.IsEmailConfirmedAsync(user)))
                //{
                //    _jsonConfirm.AlertMessage = "User email is not yet confirmed. Please confirm email first. " + model.Email;
                //    _jsonConfirm.IsSuccess = false;
                //    return new JsonResult(jsonConfirm);
                //}

                //var appUrl = $"{Request.Scheme}://{Request.Host}";
                //var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(passwordResetToken));
                //var callBackLink = $"{appUrl}/confirmemail";
                //var passwordResetEmailSent =
                //    await _authService.SendPasswordResetEmailAsync(user, callBackLink);

                //_jsonConfirm.AlertMessage = "Success, . Email: " + model.Email;
                //_jsonConfirm.IsSuccess = true;
                //_jsonConfirm.EmailSent = DateTime.Now;

                //return new JsonResult(_jsonConfirm);

                //var errors = ModelState.SelectMany(x => x.Value.Errors.Select(xx => xx.ErrorMessage));
                //var error = string.Join(" ", errors);
                //return BadRequest(new { error = error });
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new { error = "Email does not exist" });
            }

            var appUrl = $"{Request.Scheme}://{Request.Host}";
            var passwordResetEndpoint = $"{appUrl}/confirmemail";
            var passwordResetEmailSent =
                await _authService.SendPasswordResetEmailAsync(user, passwordResetEndpoint);

            if (passwordResetEmailSent)
            {
                return Ok(new
                {
                    message =
                        "A reset password link has been sent to the email provided. Please go to your inbox and click on the link to reset your password"
                });
            }

            return BadRequest(new
            {
                message = "Failed to send a reset password link. Please try again"
            });


        }


        [HttpGet("ResetPassword")]
        public IActionResult ResetPassword(string token, string Email)
        {
            var viewModel = new ResetPasswordDto();
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { errorToken = "" });
            }

            if (string.IsNullOrEmpty(Email))
            {
                return BadRequest(new { errorEmail = "Email is required" });
            }

            viewModel.Token = token;
            viewModel.Email = Email;

            return Ok(viewModel);
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return Ok(new { message = "Password reset successfully" });
                    }

                    var errors = result.Errors.Select(err => new { code = err.Code, description = err.Description });
                    return BadRequest(new { errors = errors });
                }
                else
                {
                    return BadRequest(new { errorEmail = "Email not found" });
                }
            }

            return BadRequest(new { error = "Invalid model state" });
        }


        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);

        }


        

    }
}
