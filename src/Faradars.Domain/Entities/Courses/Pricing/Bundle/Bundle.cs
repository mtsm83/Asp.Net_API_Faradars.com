using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Media;
using Faradars.Domain.Enums;

namespace Faradars.Domain.Entities.Courses.Pricing.Bundle;

public class Bundle : BaseEntity
{
    public string Title { get; set; } = null!;
    public int? BannerImageId { get; set; }
    public string? Description { get; set; }
    // how much will be discount while purchasing this group/ bundle
    public decimal? DiscountValue { get; set; } // 20% or 990,000 Rials
    public DiscountType? DiscountType { get; set; } // percentage, fixed amount
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }

    public AssetFile BannerImage { get; set; } = null!;
    public ICollection<BundleCourse> BundleItems { get; set; } = new List<BundleCourse>();
}