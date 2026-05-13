namespace Faradars.Application.DTOs.Courses.Tag.TagService;

public class TagDto
{
    public int TagId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
}