using Faradars.Application.DTOs.Courses.Category.CategoryService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Category;

public interface ICategoryService
{
    Task<Result<CategoryDto>> AddCategoryAsync(AddCategoryDto dto, CancellationToken ct);
    Task<Result<CategoryDto>> UpdateCategoryAsync(UpdateCategoryDto dto, CancellationToken ct);
    Task<Result<CategoryDto>> DeleteCategoryAsync(int categoryId, CancellationToken ct);
    Task<Result<Unit>> CategoryCourseAssignmentAsync(int courseId, int categoryId, CancellationToken ct);
    Task<Result<List<CategoryDto>>> GetAllCategoriesAsync(CancellationToken ct);
    Task<Result<CategoryDto>> GetCategoryByIdAsync(int categoryId, CancellationToken ct);

    Task<Result<CategoryDto>>
        GetCategoryWithChildrenByIdAsync(int categoryId, CancellationToken ct);
}