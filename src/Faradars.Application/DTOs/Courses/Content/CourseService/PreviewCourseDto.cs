namespace Faradars.Application.DTOs.Courses.Content.CourseService;

/// <summary>
/// for users who want to see information of courses
/// must not include administrative information 
/// </summary>
public class PreviewCourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Level { get; set; } 
    public string Language { get; set; } 
    public bool IsPublished { get; set; }
    public TimeSpan? TotalDuration { get; set; }
    public double AverageRating { get; set; }
    public int TeacherId { get; set; }
}