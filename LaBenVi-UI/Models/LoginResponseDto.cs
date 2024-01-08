namespace LaBenVi_UI.Models
{
    public class LoginResponseDto
    {
        public AppUserDto? User { get; set; }
        public string? Token { get; set; }
    }


    public class AppUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public IList<string> RoleName { get; set; }
    }
}
