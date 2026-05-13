namespace Faradars.Application.DTOs.Courses.Category.CategoryService;

public class UpdateCategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
}