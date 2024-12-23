
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketEndpoint : ICarterModule
    {
        public record StoreBasketRequest(ShoppingCart cart);
        public record StoreBasketResponse(string UserName);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async ([FromBody]StoreBasketRequest request,ISender sender) =>
            {
                var result = await sender.Send(new StoreBasketCommand(request.cart));
                return Results.Created($"/basket/{result.UserName}",result);
            });

        }
    }
}
