namespace LaBenVi_AuthService.Models.Dto
{
    public class AppUserDto
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public IList<string> RoleName { get; set; }
    }
}
