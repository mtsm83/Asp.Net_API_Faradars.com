using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Media;

namespace Faradars.Domain.Entities.Courses.Content;

public class CourseImage : BaseEntity
{
    public int CourseId { get; set; }
    public int ImageId { get; set; }
    public bool IsCover { get; set; } = false;
    
    public Course Course { get; set; } = null!;
    public AssetFile Image { get; set; } = null!;
    
}