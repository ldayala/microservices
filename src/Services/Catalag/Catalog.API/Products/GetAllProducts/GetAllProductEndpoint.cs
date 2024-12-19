using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetAllProducts
{
    public record GetProductResponse(IEnumerable<Product> Products);
    public record Parameters(int? PageNumber, int? PageSize);
    public class GetAllProductEndpoint : CarterModule
    {
       
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] Parameters para, ISender sender ) =>
            {
                
                var result = await sender.Send(new GetProductsQuery(para.PageNumber??1,para.PageSize??10));
                //var response = result.Adapt<GetProductResponse>();

                return Results.Ok(result);
            })
                .WithName("GetProducts")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Products")
                .WithDescription("Get Products");
        }
    }
}
