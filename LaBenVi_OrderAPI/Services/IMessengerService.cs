

using LaBenVi_OrderAPI.Models;

namespace LaBenVi_OrderAPI.Services
{
    public interface IMessengerService
    {
        Task<bool> Send(EmailLogger message, string attachment = "");
    }
}
