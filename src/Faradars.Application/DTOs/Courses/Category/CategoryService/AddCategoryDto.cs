namespace Faradars.Application.DTOs.Courses.Category.CategoryService;

public class AddCategoryDto
{
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
}