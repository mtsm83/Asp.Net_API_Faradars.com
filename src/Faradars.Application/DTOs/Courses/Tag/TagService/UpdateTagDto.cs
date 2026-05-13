namespace Faradars.Application.DTOs.Courses.Tag.TagService;

public class UpdateTagDto
{
    public int TagId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}