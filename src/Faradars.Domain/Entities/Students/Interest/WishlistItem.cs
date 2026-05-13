using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Domain.Entities.Students.Interest;

public class WishlistItem : BaseEntity
{
    public int UserId { get; set; }
    public int CourseId { get; set; }

    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
}