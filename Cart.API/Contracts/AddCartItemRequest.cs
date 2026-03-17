using System.ComponentModel.DataAnnotations;

namespace Cart.API.Contracts;

public class AddCartItemRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }

    [Required]
    [MinLength(1)]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    [Range(1, 10_000)]
    public int Quantity { get; set; }

    [Required]
    [Range(0.01, (double)decimal.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [MinLength(1)]
    public string Currency { get; set; } = "EUR";
}
