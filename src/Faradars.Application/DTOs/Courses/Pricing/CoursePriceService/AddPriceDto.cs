using Faradars.Domain.Enums;

namespace Faradars.Application.DTOs.Courses.Pricing.CoursePriceService;

public class AddPriceDto
{
    public int CourseId { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal? DiscountValue { get; set; } // 20% or 990,000 Rials
    public DiscountType? DiscountType { get; set; }

    // public decimal CurrentPrice { get; set; } 
}