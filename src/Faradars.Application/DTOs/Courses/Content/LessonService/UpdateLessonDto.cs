namespace Faradars.Application.DTOs.Courses.Content.LessonService;

public class UpdateLessonDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int SectionId { get; set; }
    public string Name { get; set; } = null!;
    public int Order { get; set; }
    public string? Description { get; set; }
    public bool IsFree { get; set; }
}