namespace Faradars.Application.DTOs.Courses.Pricing.DiscountService;

public class AddCouponUsageDto
{
    public int CouponId { get; set; }
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public string? IpAddress { get; set; } 
}