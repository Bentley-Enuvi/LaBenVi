namespace LaBenVi_AuthService.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
        public int Code { get; set; }
        public string Error { get; set; }
    }
}
