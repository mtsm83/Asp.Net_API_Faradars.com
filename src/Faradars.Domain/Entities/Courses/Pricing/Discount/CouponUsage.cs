using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Payments.Order;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Domain.Entities.Courses.Pricing.Discount;

public class CouponUsage : BaseEntity
{
    public int CouponId { get; set; }
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime UsedAt { get; set; }
    public string? IpAddress { get; set; } // IP address for fraud detection

    public Coupon Coupon { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public User User { get; set; } = null!;
}