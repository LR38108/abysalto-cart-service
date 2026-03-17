using Cart.API.Contracts;
using Cart.API.Models;

namespace Cart.API.Services;

public interface ICartService
{
    Task<ShoppingCart> CreateCartAsync(string? userId = null);
    Task<ShoppingCart?> GetCartAsync(Guid cartId);
    Task<ShoppingCart?> AddItemAsync(Guid cartId, AddCartItemRequest request);
    Task<ShoppingCart?> RemoveItemAsync(Guid cartId, int productId);
}
