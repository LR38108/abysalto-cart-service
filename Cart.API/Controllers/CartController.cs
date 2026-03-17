using Cart.API.Contracts;
using Cart.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly CartService _service;

    public CartController(CartService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCart()
    {
        var cart = await _service.CreateCartAsync();
        return CreatedAtAction(nameof(GetCart), new { id = cart.Id }, cart);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCart(Guid id)
    {
        var cart = await _service.GetCartAsync(id);
        return cart is null ? NotFound() : Ok(cart);
    }

    [HttpPost("{id:guid}/items")]
    public async Task<IActionResult> AddItem(Guid id, [FromBody] AddCartItemRequest request)
    {
        var cart = await _service.AddItemAsync(id, request);
        return cart is null ? NotFound() : Ok(cart);
    }

    [HttpDelete("{id:guid}/items/{productId:int}")]
    public async Task<IActionResult> RemoveItem(Guid id, int productId)
    {
        var cart = await _service.RemoveItemAsync(id, productId);
        return cart is null ? NotFound() : Ok(cart);
    }
}
