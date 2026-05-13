using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Payments.Order;

public class CartOrderItemType : BaseEntity
{
    public string Name { get; set; } // course, plan, anything purchasable
}