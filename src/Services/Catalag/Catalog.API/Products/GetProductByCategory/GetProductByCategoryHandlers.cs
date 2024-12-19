
using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category):IQuery<IEnumerable<Product>>;
    public class GetProductByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery,IEnumerable< Product>>
    {
        public async Task<IEnumerable<Product>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>().Where(p=>p.Category.Contains(request.category)).ToListAsync(cancellationToken);
            return product;
        }
    }
}
