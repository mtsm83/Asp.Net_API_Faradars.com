using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Payments.Order;

namespace Faradars.Domain.Entities.Payments.Cart;

public class CartItem : BaseEntity
{
    public int CartId { get; set; } 
    public int ItemId { get; set; } // CourseId, PlanId
    public int CartOrderItemTypeId { get; set; }
    
    public CartOrderItemType CartOrderItemType { get; set; }
    
}