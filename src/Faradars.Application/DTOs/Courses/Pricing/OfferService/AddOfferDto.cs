using Faradars.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Faradars.Application.DTOs.Courses.Pricing.OfferService;

public class AddOfferDto
{
    public string Title { get; set; } = null!; // "Black Friday 1402"
    public string? Description { get; set; }
    public decimal? DiscountValue { get; set; } // 20% or 990,000 Rials
    public DiscountType? DiscountType { get; set; }  // percentage, fixed amount
    public IFormFile? BannerImage { get; set; }
    public string? Slug { get; set; } // Landing page slug: "/black-friday-1402"
    public int? QuantityLimit { get; set; } // 1 course per account in this offer
    public DateTime OfferStarts { get; set; } 
    public DateTime OfferEnds { get; set; } 

}