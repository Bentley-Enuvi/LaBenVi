namespace LaBenVi_AuthService.Models.Dto
{
    public class LoginResponseDto
    {
        public AppUserDto User { get; set; }
        public string Token { get; set; }
    }
}
