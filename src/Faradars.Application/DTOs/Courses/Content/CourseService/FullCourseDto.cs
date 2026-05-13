namespace Faradars.Application.DTOs.Courses.Content.CourseService;

/// <summary>
/// only when admin wanted a comprehensive course
/// information for adjustment or view
/// </summary>
public class FullCourseDto
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
    public int CreatorId { get; set; }
    public int? UpdatorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}