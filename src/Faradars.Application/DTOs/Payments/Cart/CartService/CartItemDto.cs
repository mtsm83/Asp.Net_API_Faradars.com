namespace Faradars.Application.DTOs.Payments.Cart.CartService;

public class CartItemDto
{
    public int CartItemId { get; init; }
    public int ItemId { get; init; }
    public int CartId { get; init; }
    public int CartOrderItemTypeId { get; init; }
    public int CreatedBy { get; init; }
    public int? UpdatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    
}