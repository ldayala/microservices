using Catalog.API.DTOs;

namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var response = await sender.Send(new UpdateProductCommand(request));
                return Results.Ok(response);
            })
                .WithName("UpdateProduct")
                .WithDescription("Update product")
                .Produces<bool>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
