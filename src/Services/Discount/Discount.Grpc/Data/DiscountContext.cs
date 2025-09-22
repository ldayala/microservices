using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext:DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //creamos datos iniciales para la tabla Coupons
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    ProductName = "IPhone X",
                    Description = "IPhone Discount",
                    Amount = 150
                },
                new Coupon
                {
                    Id = 2,
                    ProductName = "Samsung 10",
                    Description = "Samsung Discount",
                    Amount = 100
                },
                new Coupon
                {
                    Id = 3,
                    ProductName = "Pixel",
                    Description = "Pixel Discount",
                    Amount = 50
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
