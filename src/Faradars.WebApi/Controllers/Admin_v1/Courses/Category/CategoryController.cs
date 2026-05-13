using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Category.CategoryService;
using Faradars.Application.Interfaces.Services.Courses.Category;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Category;

[ApiVersion("1")]
public class CategoryController(ICategoryService service) : AdminBaseController
{
    [HttpPost]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto dto, CancellationToken ct)
    {
        var result = await service.AddCategoryAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryDto dto, CancellationToken ct)
    {
        var result = await service.UpdateCategoryAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{categoryId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategoryAsync(int categoryId, CancellationToken ct)
    {
        var result = await service.DeleteCategoryAsync(categoryId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllCategoriesAsync(CancellationToken ct)
    {
        var result = await service.GetAllCategoriesAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{categoryId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCategoryByIdAsync(int categoryId, CancellationToken ct)
    {
        var result = await service.GetCategoryByIdAsync(categoryId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{categoryId:int}/with-children")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCategoryWithChildrenByIdAsync(int categoryId, CancellationToken ct)
    {
        var result = await service.GetCategoryWithChildrenByIdAsync(categoryId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("assign-course")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CategoryCourseAssignmentAsync([FromBody] int courseId, int categoryId, CancellationToken ct)
    {
        var result = await service.CategoryCourseAssignmentAsync(courseId, categoryId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
