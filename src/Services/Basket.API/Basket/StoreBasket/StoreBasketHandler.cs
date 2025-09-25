﻿
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can no be null");
            RuleFor(x=>x.Cart.UserName).NotEmpty().WithMessage("Username is Required");
        }
    }
    public class StoreBasketHandler 
        (
        IBasketRepository repository, 
       DiscountProtoService.DiscountProtoServiceClient discountClient
        )
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public  async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
           await DeductDiscount(command);
            //TODO: update cache
            await repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(command.Cart.UserName);
        }
        
        private async Task DeductDiscount(StoreBasketCommand command)
        {
            //TODO: store basket in database (use marten upsert - if exit=update,
            foreach (var item in command.Cart.Items)
            {
                var coupon = await discountClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
                item.Price -= coupon.Amount;
            }
        }
    }

}
