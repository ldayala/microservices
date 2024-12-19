﻿namespace Catalog.API.Products.GetAllProducts
{
    public record GetProductsQuery(int? PageNumber, int? PageSize) : IQuery<GetProductsResult>;
    

    public record GetProductsResult(IEnumerable<Product> Products);
    public class GetProductsQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
                    var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber??1, query.PageSize??10); ;
            return new GetProductsResult(products);
        }
    }
}
