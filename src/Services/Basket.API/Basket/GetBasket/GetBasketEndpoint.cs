namespace Basket.API.Basket.GetBasket
{
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{username}", async (string username, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(username));
                return Results.Ok(result);
            })
                .WithName("GetBasket")
                .WithSummary("Get Basket")
                .Produces<ShoppingCart>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Get Basket");

        }
    }
}
