using Discount.Grpc.Data;
using Discount.Grpc.Mappers;
using Discount.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService
        (DiscountContext discountContext,
        ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext
                  .Coupons
                  .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            coupon ??= new Coupon
            {
                Id = 0,
                ProductName = "No Discount",
                Amount = 0,
                Description = ""
            };
            logger.LogInformation("Discount is retrieved for the product: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);
            return coupon.CuponToCuponModel();

        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            if (request.Coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
            }
            var coupon = request.Coupon.CuponModelToCupon();

            discountContext.Coupons.Add(coupon);
            await discountContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully created. ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);
            return request.Coupon;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.CuponModelToCupon();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request object"));

            discountContext.Coupons.Update(coupon);
            await discountContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully updated. ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);
            return request.Coupon;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            discountContext.Coupons.Remove(coupon);
           await discountContext.SaveChangesAsync();
            return new DeleteDiscountResponse { Success = true };
        }

    }
}
