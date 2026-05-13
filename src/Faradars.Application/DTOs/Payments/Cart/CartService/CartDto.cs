namespace Faradars.Application.DTOs.Payments.Cart.CartService;

public class CartDto
{
    public int CartId { get; init; }
    public int UserId { get; init; }
    public DateTime? ConvertedToOrderAt { get; set; }
    public int? ConvertedOrderId { get; set; }
    public List<CartItemDto> Items { get; init; } = new List<CartItemDto>();
}