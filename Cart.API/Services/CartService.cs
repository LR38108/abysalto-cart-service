using Cart.API.Contracts;
using Cart.API.Data;
using Cart.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cart.API.Services;

public class CartService : ICartService
{
    private readonly CartDbContext _db;
    private readonly ICartCacheService _cache;

    public CartService(CartDbContext db, ICartCacheService cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<ShoppingCart> CreateCartAsync(string? userId = null)
    {
        var cart = new ShoppingCart
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Carts.Add(cart);
        await _db.SaveChangesAsync();
        await _cache.SetAsync(cart);

        return cart;
    }

    public async Task<ShoppingCart?> GetCartAsync(Guid cartId)
    {
        var cached = await _cache.GetAsync(cartId);
        if (cached is not null)
            return cached;

        var cart = await _db.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == cartId);

        if (cart is not null)
            await _cache.SetAsync(cart);

        return cart;
    }

    public async Task<ShoppingCart?> AddItemAsync(Guid cartId, AddCartItemRequest request)
    {
        var cart = await _db.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == cartId);

        if (cart is null)
            return null;

        var existing = cart.Items.FirstOrDefault(x => x.ProductId == request.ProductId);

        if (existing is not null)
        {
            existing.Quantity += request.Quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                CartId = cartId,
                ProductId = request.ProductId,
                ProductName = request.ProductName,
                Quantity = request.Quantity,
                Price = request.Price,
                Currency = request.Currency
            });
        }

        cart.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        await _cache.SetAsync(cart);

        return cart;
    }

    public async Task<ShoppingCart?> RemoveItemAsync(Guid cartId, int productId)
    {
        var cart = await _db.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == cartId);

        if (cart is null)
            return null;

        var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);
        if (item is not null)
        {
            cart.Items.Remove(item);
            _db.CartItems.Remove(item);
            cart.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
        }

        await _cache.SetAsync(cart);
        return cart;
    }
}
