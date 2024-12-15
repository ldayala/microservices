using Catalog.API.DTOs;
using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(UpdateProductRequest UpdateProductRequest) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSucces);
    public class UpdateProductHandler(ILogger<UpdateProductHandler> logger,IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler.Handler called with {@Query}", command);
            var product = await session.LoadAsync<Product>(command.UpdateProductRequest.Id, cancellationToken)
                ?? throw new ProductNotFoundException();
            product.Name = command.UpdateProductRequest.Name;
            product.Category = command.UpdateProductRequest.Category;
            product.Description = command.UpdateProductRequest.Description;
            product.Price =command.UpdateProductRequest.Price;
            product.ImageFile=command.UpdateProductRequest.ImageFile;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
    
}
