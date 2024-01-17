using LaBenVi_CartAPI.Models.DTOs;

namespace LaBenVi_CartAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
