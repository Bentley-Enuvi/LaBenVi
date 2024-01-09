//using LaBenVi_UI.Models;
//using LaBenVi_UI.Services.IServices;
//using LaBenVi_UI.Utilities;

//namespace LaBenVi_UI.Services
//{
//    public class OrderService : IOrderService
//    {
//        private readonly IBaseService _baseService;
//        public OrderService(IBaseService baseService)
//        {
//            _baseService = baseService;
//        }



//        public async Task<ResponseDto?> CreateOrder(CartDto cartDto)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.POST,
//                Data = cartDto,
//                Url = Static_Details.OrderAPIBase + "/api/order/CreateOrder"
//            });
//        }

//        public async Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.POST,
//                Data = stripeRequestDto,
//                Url = Static_Details.OrderAPIBase + "/api/order/CreateStripeSession"
//            });
//        }

//        public async Task<ResponseDto?> GetAllOrder(string? userId)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.GET,
//                Url = Static_Details.OrderAPIBase + "/api/order/GetOrders?userId=" + userId
//            });
//        }

//        public async Task<ResponseDto?> GetOrder(int orderId)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.GET,
//                Url = Static_Details.OrderAPIBase + "/api/order/GetOrder/" + orderId
//            });
//        }

//        public async Task<ResponseDto?> UpdateOrderStatus(int orderId, string newStatus)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.POST,
//                Data = newStatus,
//                Url = Static_Details.OrderAPIBase + "/api/order/UpdateOrderStatus/" + orderId
//            });
//        }

//        public async Task<ResponseDto?> ValidateStripeSession(int orderHeaderId)
//        {
//            return await _baseService.SendAsync(new RequestDto()
//            {
//                ApiAction = Static_Details.ApiAction.POST,
//                Data = orderHeaderId,
//                Url = Static_Details.OrderAPIBase + "/api/order/ValidateStripeSession"
//            });
//        }
//    }
//}
