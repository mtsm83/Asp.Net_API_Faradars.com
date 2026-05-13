using Faradars.Domain.Enums;

namespace Faradars.Application.DTOs.Courses.Pricing.OfferService;

public class OfferDto
{
    public int OfferId { get; set; }
    public string Title { get; set; } = null!; // "Black Friday 1402"
    public string? Description { get; set; }
    public decimal? DiscountValue { get; set; } // 20% or 990,000 Rials
    public DiscountType? DiscountType { get; set; } // percentage, fixed amount
    public int? BannerImageId { get; set; }
    public string? Slug { get; set; } // Landing page slug: "/black-friday-1402"
    public int? QuantityLimit { get; set; } // 1 course per account in this offer
    public DateTime OfferStarts { get; set; }
    public DateTime OfferEnds { get; set; }

    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
}