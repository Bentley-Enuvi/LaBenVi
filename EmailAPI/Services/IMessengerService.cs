using EmailAPI.Models;

namespace EmailAPI.Services
{
    public interface IMessengerService
    {
        Task<bool> Send(EmailLogger message, string attachment = "");
    }
}
