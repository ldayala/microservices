using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(Guid Id,string Name, List<string> Category,string Description,string ImageFile,decimal Price)
        :ICommand<CreatePorductResult>;
    public record CreatePorductResult(Guid Id);
    //esta sera nuestra capa de logina de la aplicacion
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreatePorductResult>
    {

        public async Task<CreatePorductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create product entity from command object
            var product = new Product { 
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            Id = command.Id,
            Price = command.Price,
            ImageFile = command.ImageFile,

            };

            //save to database
            //return CreateProductResult resul
            return new CreatePorductResult(Guid.NewGuid());
            throw new NotImplementedException();
        }
    }

}
