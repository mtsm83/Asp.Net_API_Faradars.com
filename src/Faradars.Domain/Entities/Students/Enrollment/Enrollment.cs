using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Domain.Entities.Students.Enrollment;

public class Enrollment : BaseEntity
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public int? PaymentId { get; set; }
    
    public Course Course { get; set; } = null!;
    public User Student { get; set; } = null!;
}