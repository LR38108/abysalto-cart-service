using Cart.API.Models;

namespace Cart.API.Services;

public interface ICartCacheService
{
    Task<ShoppingCart?> GetAsync(Guid cartId);
    Task SetAsync(ShoppingCart cart);
}
