using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Content.CourseService;
using Faradars.Application.Interfaces.Services.Courses.Content;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Content;

[ApiVersion("1")]
public class CourseController(ICourseService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddCourseAsync([FromBody] AddCourseDto dto, CancellationToken ct)
    {
        var result = await service.AddCourseAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCourseAsync([FromBody] UpdateCourseDto dto, CancellationToken ct)
    {
        var result = await service.UpdateCourseAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut("{courseId:int}/update-average-rating")]
    public async Task<IActionResult> UpdateCourseAverageRatingAsync(int courseId, CancellationToken ct)
    {
        var result = await service.UpdateCourseAverageRatingAsync(courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{courseId:int}")]
    public async Task<IActionResult> DeleteCourseAsync(int courseId, CancellationToken ct)
    {
        var result = await service.DeleteCourseAsync(courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCoursesAsync(CancellationToken ct)
    {
        var result = await service.GetAllCoursesAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetAllSearchedCoursesAsync([FromQuery] string searchText, CancellationToken ct)
    {
        var result = await service.GetAllSearchedCoursesAsync(searchText, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{courseId:int}")]
    public async Task<IActionResult> GetCourseByIdAsync(int courseId, CancellationToken ct)
    {
        var result = await service.GetCourseByIdAsync(courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("by-category/{categoryId:int}")]
    public async Task<IActionResult> GetCoursesByCategoryAsync(int categoryId, CancellationToken ct)
    {
        var result = await service.GetCoursesByCategoryAsync(categoryId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("by-tag/{tagId:int}")]
    public async Task<IActionResult> GetCoursesByTagAsync(int tagId, CancellationToken ct)
    {
        var result = await service.GetCoursesByTagAsync(tagId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("by-offer/{offerId:int}")]
    public async Task<IActionResult> GetAllOfferCoursesId(int offerId, CancellationToken ct)
    {
        var result = await service.GetAllOfferCoursesId(offerId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("by-bundle/{bundleId:int}")]
    public async Task<IActionResult> GetAllBundleCoursesAsync(int bundle, CancellationToken ct)
    {
        var result = await service.GetAllBundleCoursesAsync(bundle, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("by-subscription/{subscriptionId:int}")]
    public async Task<IActionResult> GetAllSubscriptionCoursesAsync(int subscriptionId, CancellationToken ct)
    {
        var result = await service.GetAllSubscriptionCoursesAsync(subscriptionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("by-teacher/{teacherId:int}")]
    public async Task<IActionResult> GetAllTeacherCoursesAsync(int teacherId, CancellationToken ct)
    {
        var result = await service.GetAllTeacherCoursesAsync(teacherId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
