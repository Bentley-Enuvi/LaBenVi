

using LaBenVi_AuthService.Models;

namespace LaBenVi_AuthService.Service.IService
{
    public interface IMessengerService
    {
        Task<bool> Send(EmailLogger message, string attachment = "");
    }
}
