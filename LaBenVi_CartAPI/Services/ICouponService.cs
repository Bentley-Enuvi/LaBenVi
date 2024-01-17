using LaBenVi_CartAPI.Models.DTOs;

namespace LaBenVi_CartAPI.Services
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
