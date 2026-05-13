namespace Faradars.Application.DTOs.Courses.Category.CategoryService;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
    public List<CategoryDto>? Children { get; set; } = new List<CategoryDto>();
}