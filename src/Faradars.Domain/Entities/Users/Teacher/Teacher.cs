using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Domain.Entities.Users.Teacher;

public class Teacher : BaseEntity
{
    public int UserId { get; set; }
    public string? Bio { get; set; }
    public string? LinkedinUrl { get; set; }
    public string? WebsiteUrl { get; set; }

    public User User { get; set; } = null!;
    public ICollection<Course> Teaches { get; set; } = new List<Course>();
}