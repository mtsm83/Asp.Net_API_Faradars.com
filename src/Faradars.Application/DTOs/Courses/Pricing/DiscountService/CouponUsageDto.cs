namespace Faradars.Application.DTOs.Courses.Pricing.DiscountService;

public class CouponUsageDto
{
    public int CouponUsageId { get; set; }
    public int CouponId { get; set; }
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime UsedAt { get; set; }
    public string? IpAddress { get; set; } 
}