namespace Faradars.Application.DTOs.Courses.Pricing.BundleService;

public class AddBundleDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public long Price { get; set; }
    public long? DiscountedPrice { get; set; }
    public int? DiscountPercentage { get; set; }
    public bool HasDiscount { get; set; }
    public bool IsActive { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? BannerImageId { get; set; }
}