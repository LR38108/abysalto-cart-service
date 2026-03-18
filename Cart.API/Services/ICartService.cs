using Cart.API.Contracts;
using Cart.API.Models;

namespace Cart.API.Services;

public interface ICartService
{
    Task<ShoppingCart> CreateCartAsync(string? userId = null, CancellationToken ct = default);
    Task<ShoppingCart?> GetCartAsync(Guid cartId, CancellationToken ct = default);
    Task<ShoppingCart?> AddItemAsync(Guid cartId, AddCartItemRequest request, CancellationToken ct = default);
    Task<ShoppingCart?> RemoveItemAsync(Guid cartId, int productId, CancellationToken ct = default);
}
