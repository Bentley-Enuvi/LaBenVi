using System.ComponentModel.DataAnnotations;

namespace LaBenVi_UI.Models
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
