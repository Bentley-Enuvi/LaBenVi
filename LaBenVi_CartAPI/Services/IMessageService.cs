namespace LaBenVi_CartAPI.Services
{
    public interface IMessageService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    }
}
