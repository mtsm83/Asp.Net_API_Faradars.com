using Faradars.Application.DTOs.Payments.Cart.CartService;
using Faradars.Domain.Entities.Payments.Cart;

namespace Faradars.Application.Mappers.Payments.Cart;

public static class CartServiceMappers
{
    public static CartDto MapToCartDto(this Domain.Entities.Payments.Cart.Cart entity)
    {
        return new CartDto
        {
            CartId = entity.Id,
            UserId = entity.UserId,
            ConvertedToOrderAt = entity.ConvertedToOrderAt,
        };
    }

    public static CartItemDto MapToCartItemDto(this CartItem entity)
    {
        return new CartItemDto
        {
            CartItemId = entity.Id,
            CartId = entity.CartId,
            ItemId = entity.ItemId,
            CartOrderItemTypeId = entity.CartOrderItemTypeId,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }
}