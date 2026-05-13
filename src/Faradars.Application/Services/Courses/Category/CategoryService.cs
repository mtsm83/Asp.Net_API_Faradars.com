using Faradars.Application.DTOs.Courses.Category.CategoryService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Category;
using Faradars.Application.Mappers.Courses.Category;
using Faradars.Domain.Entities.Courses.Category;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Category;

public class CategoryService(
    IRepository<Domain.Entities.Courses.Category.Category> categoryRepository,
    IRepository<Course> courseRepository,
    IRepository<CourseCategory> courseCategoryRepository,
    IUserContextService userContextService
) : ICategoryService, IScopedDependency
{
    public async Task<Result<CategoryDto>> AddCategoryAsync(AddCategoryDto dto, CancellationToken ct)
    {
        if (dto.ParentId != null)
        {
            var parentCategory = await categoryRepository.GetByIdAsync(ct, dto.ParentId);
            if (parentCategory == null)
                return Result.Failure<CategoryDto>(Error.ParentNotFound); // Parent category doesn't exist
        }

        var newCategory = dto.MapAddDtoToCategory();
        newCategory.CreatedBy = userContextService.CurrentUser.UserId;
        await categoryRepository.AddAsync(newCategory, ct);
        var categoryDto = newCategory.MapCategoryToDto();
        return Result.Success(categoryDto);
    }

    public async Task<Result<CategoryDto>> UpdateCategoryAsync(UpdateCategoryDto dto, CancellationToken ct)
    {
        var category = await categoryRepository.GetByIdAsync(ct, dto.CategoryId);
        if (category == null)
            return Result.Failure<CategoryDto>(Error.NotFound);

        if (dto.ParentId != null)
        {
            var parentCategory = await categoryRepository.GetByIdAsync(ct, dto.ParentId);
            if (parentCategory == null)
                return Result.Failure<CategoryDto>(Error.ParentNotFound); // Parent category doesn't exist
        }

        category.MapUpdateDtoToCategory(dto);
        await categoryRepository.UpdateAsync(category, ct);
        var categoryDto = category.MapCategoryToDto();
        return Result.Success(categoryDto);
    }

    public async Task<Result<CategoryDto>> DeleteCategoryAsync(int categoryId, CancellationToken ct)
    {
        var category = await categoryRepository.Table
            .Include(c => c.Children)
            .FirstOrDefaultAsync(c => c.Id == categoryId, ct);
        if (category == null)
            return Result.Failure<CategoryDto>(Error.CategoryIdDoesNotExist);

        await SoftDeleteCategoryExtension(category, ct);
        var dto = category.MapCategoryToDto();
        return Result.Success(dto);
    }


    public async Task<Result<CategoryDto>> GetCategoryWithChildrenByIdAsync(int categoryId, CancellationToken ct)
    {
        var category = await categoryRepository.TableNoTracking
            .Include(c => c.Children)
            .FirstOrDefaultAsync(c => c.Id == categoryId, ct);
        if (category == null)
            return Result.Failure<CategoryDto>(Error.NotFound);

        var categoryDto = category.MapCategoryToDto();
        return Result.Success(categoryDto);
    }

    public async Task<Result<Unit>> CategoryCourseAssignmentAsync(int courseId, int categoryId, CancellationToken ct)
    {
        var category = await categoryRepository.GetByIdAsync(ct, categoryId);
        if (category == null)
            return Result.Failure<Unit>(Error.NotFound);

        var course = await courseRepository.GetByIdAsync(ct, courseId);
        if (course == null)
            return Result.Failure<Unit>(Error.NotFound);

        var newCourseCategory = new CourseCategory
        {
            CourseId = courseId,
            CategoryId = categoryId,
            CreatedBy = userContextService.CurrentUser.UserId
        };

        await courseCategoryRepository.AddAsync(newCourseCategory, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<List<CategoryDto>>> GetAllCategoriesAsync(CancellationToken ct)
    {
        var categories = await categoryRepository.TableNoTracking
            .Include(c => c.Children).ToListAsync(ct);

        var categoryDtos = categories.Select(c => c.MapCategoryToDto()).ToList();
        return Result.Success(categoryDtos);
    }

    public async Task<Result<CategoryDto>> GetCategoryByIdAsync(int id, CancellationToken ct)
    {
        var category = await categoryRepository.GetByIdAsync(ct, id);
        if (category == null)
            return Result.Failure<CategoryDto>(Error.NotFound);

        var dto = category.MapCategoryToDto();
        return Result.Success(dto);
    }

    #region Private Methods

    private async Task SoftDeleteCategoryExtension(
        Domain.Entities.Courses.Category.Category category,
        CancellationToken ct)
    {
        var relatedCourseCategories = await courseCategoryRepository.Table
            .Where(cc => cc.CategoryId == category.Id)
            .ToListAsync(ct);

        foreach (var cc in relatedCourseCategories)
            await courseCategoryRepository.DeleteAsync(cc, ct);

        if (category.Children.Count > 0)
        {
            foreach (var child in category.Children)
            {
                await categoryRepository.Table
                    .Where(c => c.Id == child.Id)
                    .Include(c => c.Children)
                    .LoadAsync(ct);

                await SoftDeleteCategoryExtension(child, ct);
            }
        }

        await categoryRepository.DeleteAsync(category, ct);
    }

    #endregion
}