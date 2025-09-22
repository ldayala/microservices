using Discount.Grpc.Models;

namespace Discount.Grpc.Mappers
{
    public static class Mappers
    {
        public static CouponModel CuponToCuponModel(this Coupon coupon)
        {
            return new CouponModel
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };
        }

        public static Coupon CuponModelToCupon(this CouponModel coupon)
        {
            return new Coupon
            {
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount
            };
        }
    }
}
