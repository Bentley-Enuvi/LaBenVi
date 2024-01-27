using System.ComponentModel.DataAnnotations;

namespace LaBenVi_AuthService.Models.Dto
{
    public class RegRequestDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string? Role { get; set; }
    }
}
