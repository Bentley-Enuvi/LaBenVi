using LaBenVi_UI.Models;
using LaBenVi_UI.Services.IServices;
using LaBenVi_UI.Utilities;

namespace LaBenVi_UI.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.POST,
                Data = productDto,
                Url = Static_Details.ProductAPIBase + "/api/product",
                ContentType = Static_Details.ContentType.MultipartFormData
            });
        }

        public async Task<ResponseDto?> DeleteProductsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.DELETE,
                Url = Static_Details.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<ResponseDto?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.GET,
                Url = Static_Details.ProductAPIBase + "/api/product"
            });
        }



        public async Task<ResponseDto?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.GET,
                Url = Static_Details.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.PUT,
                Data = productDto,
                Url = Static_Details.ProductAPIBase + "/api/product",
                ContentType = Static_Details.ContentType.MultipartFormData
            });
        }
    }
}
