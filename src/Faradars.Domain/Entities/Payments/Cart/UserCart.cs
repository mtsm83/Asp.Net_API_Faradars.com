using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Domain.Entities.Payments.Cart;

public class UserCart : BaseEntity
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    
    public User Student { get; set; } = null!;
    public Course Course { get; set; } = null!;
}