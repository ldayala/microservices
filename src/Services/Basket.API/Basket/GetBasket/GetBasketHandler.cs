using Basket.API.Data;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName):IQuery<ShoppingCart>;
    public class GetBasketQueryHandler 
        (IBasketRepository repository)
        : IQueryHandler<GetBasketQuery, ShoppingCart>
    {
        public async Task<ShoppingCart> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(request.UserName);
            return basket;
        }
    }
}
