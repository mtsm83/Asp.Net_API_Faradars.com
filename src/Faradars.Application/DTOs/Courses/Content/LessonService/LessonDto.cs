namespace Faradars.Application.DTOs.Courses.Content.LessonService;

public class LessonDto
{
    public int CourseId { get; set; }
    public int SectionId { get; set; }
    public int LessonId { get; set; }
    public string Name { get; set; } = null!;
    public TimeSpan? Duration { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
}