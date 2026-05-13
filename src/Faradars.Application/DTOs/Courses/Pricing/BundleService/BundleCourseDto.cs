namespace Faradars.Application.DTOs.Courses.Pricing.BundleService;

public class BundleCourseDto
{
    public int Id { get; set; }
    public int BundleId { get; set; }
    public int CourseId { get; set; }
    public string? CourseTitle { get; set; }
    public long? DisplayOrder { get; set; }
    public DateTime AddedAt { get; set; }
}