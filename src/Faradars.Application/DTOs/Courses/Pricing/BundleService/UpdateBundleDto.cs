using Faradars.Domain.Enums;

namespace Faradars.Application.DTOs.Courses.Pricing.BundleService;

public class UpdateBundleDto
{
    public int BundleId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal? DiscountValue { get; set; } // 20% or 990,000 Rials
    public DiscountType? DiscountType { get; set; } // percentage, fixed amount
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public int? BannerImageId { get; set; }
}