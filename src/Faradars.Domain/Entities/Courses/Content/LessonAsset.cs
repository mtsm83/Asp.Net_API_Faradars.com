using System.ComponentModel.DataAnnotations.Schema;
using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Media;

namespace Faradars.Domain.Entities.Courses.Content;

public class LessonAsset : BaseEntity
{
    public int LessonId { get; set; }
    public int AssetFileId { get; set; }
    public string AssetFileName { get; set; } = null!;
    
    [ForeignKey("AssetFileId")] public AssetFile AssetFile { get; set; } = null!;
    [ForeignKey("LessonId")] public Lesson Lesson { get; set; } = null!;
    
}