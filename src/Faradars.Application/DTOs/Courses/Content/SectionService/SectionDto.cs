namespace Faradars.Application.DTOs.Courses.Content.SectionService;

public class SectionDto
{
    public int SectionId { get; set; }
    public int CourseId { get; set; }
    public string Name { get; set; } = null!;
    public int Order { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
}