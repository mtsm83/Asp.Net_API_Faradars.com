namespace Faradars.Application.DTOs.Payments.Cart.CartService;

public class RemoveFromCartDto
{
    public int CartId { get; init; }
    public int? ItemId { get; init; }   
}