using Cart.API.Contracts;
using Cart.API.Models;
using Cart.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest? request = null, CancellationToken ct = default)
    {
        var cart = await _service.CreateCartAsync(request?.UserId, ct);
        var response = MapToResponse(cart);
        return CreatedAtAction(nameof(GetCart), new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCart(Guid id, CancellationToken ct = default)
    {
        var cart = await _service.GetCartAsync(id, ct);
        return cart is null ? NotFound() : Ok(MapToResponse(cart));
    }

    [HttpPost("{id:guid}/items")]
    public async Task<IActionResult> AddItem(Guid id, [FromBody] AddCartItemRequest request, CancellationToken ct = default)
    {
        var cart = await _service.AddItemAsync(id, request, ct);
        return cart is null ? NotFound() : Ok(MapToResponse(cart));
    }

    [HttpDelete("{id:guid}/items/{productId:int}")]
    public async Task<IActionResult> RemoveItem(Guid id, int productId, CancellationToken ct = default)
    {
        var cart = await _service.RemoveItemAsync(id, productId, ct);
        return cart is null ? NotFound() : Ok(MapToResponse(cart));
    }

    private static CartResponse MapToResponse(ShoppingCart cart) => new()
    {
        Id = cart.Id,
        UserId = cart.UserId,
        CreatedAt = cart.CreatedAt,
        UpdatedAt = cart.UpdatedAt,
        Items = cart.Items.Select(i => new CartItemResponse
        {
            ProductId = i.ProductId,
            ProductName = i.ProductName,
            Quantity = i.Quantity,
            Price = i.Price,
            Currency = i.Currency
        }).ToList()
    };
}
