﻿

using Catalog.API.Exceptions;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSucces);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
        }
        public class DeleteProductHandler
            (IDocumentSession session, ILogger<DeleteProductHandler> logger)
            : ICommandHandler<DeleteProductCommand, DeleteProductResult>
        {
            public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                logger.LogInformation("DeleteProductHandler.Handler called with {@Query}", request.Id);

                _ = await session.LoadAsync<Product>(request.Id,cancellationToken)
                    ?? throw new ProductNotFoundException();

                session.Delete<Product>(request.Id);

                await session.SaveChangesAsync(cancellationToken);
                return new DeleteProductResult(true);
            }
        }
    }
}
