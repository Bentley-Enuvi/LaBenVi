using System.ComponentModel.DataAnnotations;

namespace LaBenVi_AuthService.Models.Dto
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
