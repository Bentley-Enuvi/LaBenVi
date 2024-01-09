using LaBenVi_UI.Models;

namespace LaBenVi_UI.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
