using Cart.API.Models;

namespace Cart.API.Services;

public interface ICartCacheService
{
    Task<ShoppingCart?> GetAsync(Guid cartId, CancellationToken ct = default);
    Task SetAsync(ShoppingCart cart, CancellationToken ct = default);
    Task RemoveAsync(Guid cartId, CancellationToken ct = default);
}
