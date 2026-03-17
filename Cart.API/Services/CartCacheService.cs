using Cart.API.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Cart.API.Services;

public class CartCacheService : ICartCacheService
{
    private readonly IDatabase _redis;
    private static readonly TimeSpan Ttl = TimeSpan.FromMinutes(30);

    public CartCacheService(IConnectionMultiplexer multiplexer)
    {
        _redis = multiplexer.GetDatabase();
    }

    public async Task<ShoppingCart?> GetAsync(Guid cartId)
    {
        var value = await _redis.StringGetAsync(GetKey(cartId));

        if (value.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<ShoppingCart>((string)value!);
    }

    public async Task SetAsync(ShoppingCart cart)
    {
        var payload = JsonSerializer.Serialize(cart);
        await _redis.StringSetAsync(GetKey(cart.Id), payload, Ttl);
    }

    public async Task RemoveAsync(Guid cartId)
    {
        await _redis.KeyDeleteAsync(GetKey(cartId));
    }

    private static string GetKey(Guid cartId) => $"cart:{cartId}";
}
