using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;

namespace Faradars.Domain.Entities.Courses.Category;

public class CourseCategory : BaseEntity
{
    public int CourseId { get; set; }
    public int CategoryId { get; set; }
    
    public Course Course { get; set; } = null!;
    public Category Category { get; set; } = null!;
}