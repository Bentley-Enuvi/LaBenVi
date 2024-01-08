using Microsoft.AspNetCore.Identity;

namespace LaBenVi_AuthService.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
