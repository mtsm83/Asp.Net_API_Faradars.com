using Faradars.Domain.Enums;

namespace Faradars.Application.DTOs.Courses.Pricing.CoursePriceService;

public class CoursePriceDto
{
    public int CourseId { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal? DiscountValue { get; set; } 
    public DiscountType? DiscountType { get; set; } 
    public decimal CurrentPrice { get; set; } 
    
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
}