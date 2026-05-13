namespace Faradars.Application.DTOs.Courses.Content.CourseService;

public class UpdateCourseDto
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = null!;
    public string Level { get; set; } = null!;
    public string? Language { get; set; }
    public string TargetAudience { get; set; } = null!;
    public string CourseType { get; set; } = null!;
    public bool? IsPublished { get; set; }
    public int TeacherId { get; set; }
}