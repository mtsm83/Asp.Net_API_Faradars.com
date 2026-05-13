using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;

namespace Faradars.Domain.Entities.Courses.Tag;

public class CourseTag : BaseEntity
{
    public int CourseId { get; set; }
    public int TagId { get; set; }
    
    public Course Course { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}