using Faradars.Domain.Enums;

namespace Faradars.Application.DTOs.Courses.Pricing.BundleService;

public class BundleDto
{
    public int BundleId { get; set; }
    public string Title { get; set; } = null!;
    public int? BannerImageId { get; set; }
    public string? Description { get; set; }
    // how much will be discount while purchasing this group/ bundle
    public decimal? DiscountValue { get; set; } // 20% or 990,000 Rials
    public DiscountType? DiscountType { get; set; } // percentage, fixed amount
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
}