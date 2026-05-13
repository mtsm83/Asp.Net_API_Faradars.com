using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Enums;

namespace Faradars.Domain.Entities.Courses.Pricing.CoursePrice;

public class CoursePrice : BaseEntity
{
    public int CourseId { get; set; }

    // The actual price in Rials
    public decimal OriginalPrice { get; set; }
    public decimal? DiscountValue { get; set; } // 20% or 990,000 Rials
    public DiscountType? DiscountType { get; set; }  // percentage, fixed amount
    // must be filled by system after adding discount (same as original if no discount)
    public decimal CurrentPrice { get; set; } 

    // Why was this price set? "Regular price", "Black Friday", 
    // "Instructor promotion" by Admin
    public string? Description { get; set; }

    public Course Course { get; set; } = null!;
}