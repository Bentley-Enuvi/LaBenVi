using LaBenVi_UI.Models;
using LaBenVi_UI.Services.IServices;
using LaBenVi_UI.Utilities;

namespace LaBenVi_UI.Services
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;
        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AddCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.POST,
                Data = cartDto,
                Url = Static_Details.CartAPIBase + "/api/cart/ApplyCoupon"
            });
        }

        public async Task<ResponseDto?> EmailCart(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.POST,
                Data = cartDto,
                Url = Static_Details.CartAPIBase + "/api/cart/EmailCartRequest"
            });
        }

        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.GET,
                Url = Static_Details.CartAPIBase + "/api/cart/GetCart/" + userId
            });
        }


        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.POST,
                Data = cartDetailsId,
                Url = Static_Details.CartAPIBase + "/api/cart/RemoveCart"
            });
        }


        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.POST,
                Data = cartDto,
                Url = Static_Details.CartAPIBase + "/api/cart/CartUpsert"
            });
        }
    }
}
