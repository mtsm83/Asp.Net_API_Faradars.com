using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Courses.Category;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
    
    public Category? Parent { get; set; }
    public ICollection<Category> Children { get; set; } = new List<Category>();
    public ICollection<CourseCategory> CourseCategories { get; set; } = new List<CourseCategory>();
}