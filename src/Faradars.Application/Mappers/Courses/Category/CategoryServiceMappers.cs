using Faradars.Application.DTOs.Courses.Category.CategoryService;

namespace Faradars.Application.Mappers.Courses.Category;

public static class CategoryServiceMappers
{
    public static Domain.Entities.Courses.Category.Category MapAddDtoToCategory(this AddCategoryDto dto)
    {
        var newCategory = new Domain.Entities.Courses.Category.Category
        {
            Name = dto.Name,
            ParentId = dto.ParentId ?? null
        };
        return newCategory;
    }

    public static void MapUpdateDtoToCategory(this Domain.Entities.Courses.Category.Category category,
        UpdateCategoryDto dto)
    {
        category.Name = dto.Name;
        category.ParentId = dto.ParentId ?? null;
    }

    public static CategoryDto MapCategoryToDto(this Domain.Entities.Courses.Category.Category category)
    {
        var dto = new CategoryDto();
        dto.Id = category.Id;
        dto.Name = category.Name;
        dto.ParentId = category.ParentId;
        dto.Children = category.Children.Select(c => c.MapCategoryToDto()).ToList();
        return dto;
    }
}