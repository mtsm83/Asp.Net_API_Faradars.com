using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Payments.Order;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int CartOrderItemTypeId { get; set; }
    public decimal Price { get; set; } // calculates & receives from pricing unit 
    
    public Order Order { get; set; }
    public CartOrderItemType CartOrderItemType { get; set; }
    
    
}

