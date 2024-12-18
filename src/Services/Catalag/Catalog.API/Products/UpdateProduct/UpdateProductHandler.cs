using Catalog.API.DTOs;
using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(UpdateProductRequest UpdateProductRequest) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSucces);

    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(com => com.UpdateProductRequest.Id).NotEmpty().WithMessage("Product Id is required");
            RuleFor(com => com.UpdateProductRequest.Name)
                .NotEmpty().WithMessage("Product name id required")
                .Length(2,50).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(com => com.UpdateProductRequest.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
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
