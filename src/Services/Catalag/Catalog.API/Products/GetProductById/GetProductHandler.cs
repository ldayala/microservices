﻿
using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductQuery(Guid id):IQuery<Product>;
       
    public class GetProductHandler(IDocumentSession session, ILogger<GetProductHandler> logger) : IQueryHandler<GetProductQuery,Product>
    {
        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductQueryHandler.Handle called with {@Query}", request);
          var product= await session.Query<Product>().FirstOrDefaultAsync(x=>x.Id==request.id,cancellationToken);
            if (product==null)
            {
                throw new ProductNotFoundException(typeof(Product).Name,request.id);
            }
            return product;
        }
    }
}
