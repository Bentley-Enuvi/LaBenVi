using LaBenVi_OrderAPI.Models.DTOs;

namespace LaBenVi_OrderAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
