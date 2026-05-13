namespace Faradars.Application.DTOs.Payments.Cart.CartService;

public class AddToCartDto
{
    public int CartId { get; init; }
    public int ItemId { get; set; } // CourseId, PlanId
    public int CartOrderItemTypeId { get; set; }
}