

namespace Catalog.API.Products.GetProductByCategory
{
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var products = await sender.Send(new GetProductByCategoryQuery(category));
                return products;
            })
                .WithName("GetProductByCategory")
                .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithDescription("Get products by category");
        }
    }
}
