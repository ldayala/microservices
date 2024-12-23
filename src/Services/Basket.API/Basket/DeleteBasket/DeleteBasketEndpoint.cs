

namespace Basket.API.Basket.DeleteBasket
{
    //public record DeleteBasketRequest(string UserName);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{user}", async (string user, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCommand(user));
                return Results.Ok(result);
            })
              .WithDescription("Delete Basket")
              .WithName("DeleteBasket")
              .Produces<bool>(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
