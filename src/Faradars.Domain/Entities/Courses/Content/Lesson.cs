using System.ComponentModel.DataAnnotations.Schema;
using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Courses.Content;

public class Lesson : BaseEntity
{
    public int CourseId { get; set; }
    public int SectionId { get; set; }
    public string Name { get; set; } = null!;
    public int Order { get; set; } // like 1,2,3
    public string? Description { get; set; }
    public TimeSpan? Duration { get; set; } = null;
    public bool IsFree { get; set; }
    
    [ForeignKey("SectionId")] public Section Section { get; set; } = null!;
    [ForeignKey("CourseId")] public Course Course { get; set; } = null!;
    public ICollection<LessonAsset> LessonAssets { get; set; } = new List<LessonAsset>();
    
}