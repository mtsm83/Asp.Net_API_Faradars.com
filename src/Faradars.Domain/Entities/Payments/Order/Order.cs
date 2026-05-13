using System.ComponentModel.DataAnnotations.Schema;
using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Pricing.Discount;
using Faradars.Domain.Enums;

namespace Faradars.Domain.Entities.Payments.Order;

public class Order : BaseEntity
{
    // AddedAt = CreatedAt

    public int CartId { get; set; }
    public string OrderNumber { get; set; } = null!; // ORD-20251225-00123
    public OrderStatus Status { get; set; }

    public decimal OriginalTotalPrice { get; set; } // 100,000
    public decimal TotalDiscountAmount { get; set; } // Order * CouponUsage in OrderId (all used coupons)
    public decimal FinalPriceToPay { get; set; } // 98,000

    [ForeignKey("CartId")] public Cart.Cart Cart { get; set; } = null!;
    public ICollection<CouponUsage> CouponUsages { get; set; } = new List<CouponUsage>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}