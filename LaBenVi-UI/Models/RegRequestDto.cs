using System.ComponentModel.DataAnnotations;

namespace LaBenVi_UI.Models
{
    public class RegRequestDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
    }
}
