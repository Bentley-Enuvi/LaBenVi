namespace LaBenVi_AuthService.Models.Dto
{
    public class RegRequestDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        //public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string? RoleName { get; set; }
    }
}
