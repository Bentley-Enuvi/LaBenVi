//using LaBenVi_UI.Models;
//using LaBenVi_UI.Services.IServices;
//using LaBenVi_UI.Utilities;

//namespace LaBenVi_UI.Services
//{
//    public class CartService : ICartService
//    {
//        private readonly IBaseService _baseService;
//        public CartService(IBaseService baseService)
//        {
//            _baseService = baseService;
//        }

//        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.POST,
//                Data = cartDto,
//                Url = Static_Details.ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
//            });
//        }

//        public async Task<ResponseDto?> EmailCart(CartDto cartDto)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.POST,
//                Data = cartDto,
//                Url = Static_Details.ShoppingCartAPIBase + "/api/cart/EmailCartRequest"
//            });
//        }

//        public async Task<ResponseDto?> GetCartByUserIdAsnyc(string userId)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.GET,
//                Url = Static_Details.ShoppingCartAPIBase + "/api/cart/GetCart/" + userId
//            });
//        }


//        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.POST,
//                Data = cartDetailsId,
//                Url = Static_Details.ShoppingCartAPIBase + "/api/cart/RemoveCart"
//            });
//        }


//        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.POST,
//                Data = cartDto,
//                Url = Static_Details.ShoppingCartAPIBase + "/api/cart/CartUpsert"
//            });
//        }
//    }
//}
