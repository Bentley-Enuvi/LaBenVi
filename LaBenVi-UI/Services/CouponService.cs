using LaBenVi_UI.Models;
using LaBenVi_UI.Services.IServices;
using LaBenVi_UI.Utilities;

namespace LaBenVi_UI.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }



        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.POST,
                Data = couponDto,
                Url = Static_Details.CouponAPIBase + "/api/coupon"
            });
        }


        public async Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.GET,
                Url = Static_Details.CouponAPIBase + "/api/coupon/" + id
            });
        }



        public async Task<ResponseDto?> GetCouponByCodeAsync(string couponCode)
        {
            var coupon = await _baseService.SendAsync(new RequestDto
            {
                ApiAction = Static_Details.ApiAction.GET,
                Url = Static_Details.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode
            });
            return coupon;

        }



        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.PUT,
                Data = couponDto,
                Url = Static_Details.CouponAPIBase + "/api/coupon"
            });
        }



        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.GET,
                Url = Static_Details.CouponAPIBase + "/api/coupon"
            });

        }



        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiAction = Static_Details.ApiAction.DELETE,
                Url = Static_Details.CouponAPIBase + "/api/coupon/" + id
            });
        }

    }
}
