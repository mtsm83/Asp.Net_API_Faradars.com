using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Courses.Tag;

public class Tag : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    
    public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
}