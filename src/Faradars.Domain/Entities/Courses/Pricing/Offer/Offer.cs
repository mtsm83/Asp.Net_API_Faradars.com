using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Media;
using Faradars.Domain.Enums;

namespace Faradars.Domain.Entities.Courses.Pricing.Offer;

public class Offer : BaseEntity
{
     public string Title { get; set; } = null!; // "Black Friday 1402"
     public string? Description { get; set; }
     public decimal? DiscountValue { get; set; } // 20% or 990,000 Rials
     public DiscountType? DiscountType { get; set; }  // percentage, fixed amount
     public int? BannerImageId { get; set; }
     public string? Slug { get; set; } // Landing page slug: "/black-friday-1402"
     public int? SoldCount { get; set; }
     public int? QuantityLimit { get; set; } // 1 course per account in this offer
     public DateTime OfferStarts { get; set; } 
     public DateTime OfferEnds { get; set; } 
   
    public AssetFile BannerImage { get; set; } = null!;
    public ICollection<OfferCourse> CourseSpecialOffers { get; set; } = new List<OfferCourse>();
    
}