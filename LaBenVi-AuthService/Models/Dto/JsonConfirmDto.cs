namespace LaBenVi_AuthService.Models.Dto
{
    public class JsonConfirmDto
    {
        public int Id { get; set; }
        public string AlertMessage { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime? EmailSent { get; set; }
    }
}
