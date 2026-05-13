using System.ComponentModel.DataAnnotations.Schema;
using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Courses.Content;

public class Section : BaseEntity
{
    public int CourseId { get; set; }
    public string Name { get; set; } = null!;
    public int Order { get; set; }
    public string? Description { get; set; }


    [ForeignKey("CourseId")] public Course Course { get; set; } = null!;
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}