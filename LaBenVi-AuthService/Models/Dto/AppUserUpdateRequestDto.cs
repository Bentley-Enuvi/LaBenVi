namespace LaBenVi_AuthService.Models.Dto
{
    public class AppUserUpdateRequestDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }
    }
}
