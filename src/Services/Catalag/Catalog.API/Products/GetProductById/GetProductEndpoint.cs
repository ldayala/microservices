
namespace Catalog.API.Products.GetProductById
{
    public class GetProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid Id, ISender sender) =>
            {
                var product= await sender.Send(new GetProductQuery(Id));
                return product;
            })
                .WithName("GetProductById")
                .WithSummary("Get product by id")
                .WithDescription("Get product by id")
                .Produces<Product>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
