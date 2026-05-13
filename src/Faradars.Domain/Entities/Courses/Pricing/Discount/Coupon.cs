using Faradars.Domain.BaseClasses;
using Faradars.Domain.Enums;

namespace Faradars.Domain.Entities.Courses.Pricing.Discount;

public class Coupon : BaseEntity
{
    // Fields which must be calculated: UsageCount
    public string Code { get; set; } // actual code user enters: "FIRST50"
    public string? Description { get; set; }
    public decimal? DiscountValue { get; set; } // 20% or 990,000 Rials
    public DiscountType? DiscountType { get; set; } // percentage, fixed amount, fixed price (99 toman all of them)
    public decimal? MaximumDiscountAmount { get; set; } // Example: 30% off up to 200,000 Tomans maximum discount
    public decimal? MinimumPurchase { get; set; } // Minimum cart total to use this coupon
    public DateTime ValidFrom { get; set; } // coupon starts from 
    public DateTime ValidTo { get; set; } // coupon ends at
    public int? UsageLimit { get; set; } // Example: First 1000 users only
    public int? UsageCount { get; set; } // How many users have used since now
}