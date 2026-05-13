namespace Faradars.Application.DTOs.Courses.Content.SectionService;

public class AddSectionDto
{
    public int CourseId { get; set; }
    public string Name { get; set; } = null!;
    public int Order { get; set; }
    public string? Description { get; set; }
}