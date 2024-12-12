
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category,string Description,string ImageFile,decimal Price)
        :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    //esta sera nuestra capa de logina de la aplicacion
    //IDocumentSession viene de la libreria Marten y es una interfaz aque implementa patrines UnitOfWork, Repository etc
    internal class CreateProductCommandHandler(IDocumentSession session) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
      
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create product entity from command object
            var product = new Product { 
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            Price = command.Price,
            ImageFile = command.ImageFile,

            };

            //save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            //return CreateProductResult resul
            return new CreateProductResult(product.Id);
           
        }
    }

}
