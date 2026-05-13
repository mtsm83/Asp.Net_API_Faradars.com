using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Media;

namespace Faradars.Domain.Entities.Courses.Content;

public class CourseIntro : BaseEntity
{
    public int CourseId { get; set; }
    public int VideoId { get; set; }
    public bool IsCover { get; set; } = false;

    public Course Course { get; set; } = null!;
    public AssetFile Video { get; set; } = null!;
}