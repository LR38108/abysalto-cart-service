namespace Cart.API.Contracts;

public class CartResponse
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<CartItemResponse> Items { get; set; } = new();
}
