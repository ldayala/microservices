
using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductQuery(Guid id):IQuery<Product>;
       
    public class GetProductHandler(IDocumentSession session) : IQueryHandler<GetProductQuery,Product>
    {
        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
          
          var product= await session.Query<Product>().FirstOrDefaultAsync(x=>x.Id==request.id,cancellationToken);
            return product ?? throw new ProductNotFoundException(request.id);
        }
    }
}
