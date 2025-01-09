using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    /*
     aqui estamos implementamos dos patrones proxy y decorator, ya que el repositorio de cestas encapsulado actua como proxy y reenvia
    las llamadas al repositorio de cestas subyacentes
     */
    public class CacheBasketRepository
        (IBasketRepository repository,
        IDistributedCache cache )
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cacheBasket = await cache.GetStringAsync( userName,cancellationToken );
            if (!string.IsNullOrEmpty(cacheBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;
           
            var basket= await repository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName,JsonSerializer.Serialize(basket),cancellationToken);

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasket(basket, cancellationToken);
            await cache.SetStringAsync(basket.UserName,JsonSerializer.Serialize(basket),cancellationToken);
            return basket;
        }
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
          await repository.DeleteBasket(userName,cancellationToken);
          await cache.RemoveAsync(userName,cancellationToken);
          return true;
        }
    }
}
