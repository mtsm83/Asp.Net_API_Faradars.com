using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;

namespace Faradars.Domain.Entities.Courses.Pricing.Bundle;

public class BundleCourse : BaseEntity
{
    public int BundleId { get; set; }
    public int CourseId { get; set; }
    public int? DisplayOrder { get; set; }
    
    public Bundle Bundle { get; set; }
    public Course Course { get; set; }
}