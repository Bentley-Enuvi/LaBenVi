using LaBenVi_CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LaBenVi_CouponAPI.Data
{
    public class LaBenViDbContext : DbContext
    {
        public LaBenViDbContext(DbContextOptions<LaBenViDbContext> options) : base(options)
        {
        }


        public DbSet<Coupon> Coupons { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 1,
                CouponCode = "967DD",
                DiscountAmount = 38,
                MinAmount = 150
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 2,
                CouponCode = "700FG",
                DiscountAmount = 40,
                MinAmount = 80
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 3,
                CouponCode = "589AF",
                DiscountAmount = 200,
                MinAmount = 400
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 4,
                CouponCode = "769FD",
                DiscountAmount = 50,
                MinAmount = 750
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                CouponId = 5,
                CouponCode = "200WS",
                DiscountAmount = 45,
                MinAmount = 950
            });
        }

    }
}
