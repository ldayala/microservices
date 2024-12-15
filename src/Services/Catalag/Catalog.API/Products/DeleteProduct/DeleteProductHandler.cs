

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSucces);
    public class DeleteProductHandler
        (IDocumentSession session,ILogger<DeleteProductHandler> logger) 
        : ICommandHandler<DeleteProductCommand,DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductHandler.Handler called with {@Query}", request.Id);
            session.Delete<Product>(request.Id);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
