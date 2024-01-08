namespace LaBenVi_CouponAPI.Models.DTOs
{
    public class UpdateCouponDto
    {
        public int CouponId { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
