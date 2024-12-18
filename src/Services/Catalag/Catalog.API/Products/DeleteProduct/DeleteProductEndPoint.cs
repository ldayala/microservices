using Catalog.API.Products.GetAllProducts;

namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductEndPoint:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                Console.WriteLine("result:", result.ToString());
                return Results.Ok(result);
            })
                .WithName("DeleteProducts")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Products")
                .WithDescription("Delete Products"); ;
        }
    }
}
