using Microsoft.AspNetCore.Identity;

namespace LaBenVi_AuthService.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string? ImageUrl { get; set; }

        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
