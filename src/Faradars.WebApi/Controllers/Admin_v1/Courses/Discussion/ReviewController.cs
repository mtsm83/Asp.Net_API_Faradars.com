using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Discussion.ReviewService;
using Faradars.Application.Interfaces.Services.Courses.Discussion;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Discussion;

[ApiVersion("1")]
public class ReviewController(IReviewService service) : AdminBaseController
{
    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddReviewAsync([FromBody] AddReviewDto dto, CancellationToken ct)
    {
        var result = await service.AddReviewAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{reviewId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteReviewAsync(int reviewId, CancellationToken ct)
    {
        var result = await service.DeleteReviewAsync(reviewId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{reviewId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetReviewByIdAsync(int reviewId, CancellationToken ct)
    {
        var result = await service.GetReviewByIdAsync(reviewId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("course/{courseId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCourseReviewsAsync(int courseId, CancellationToken ct)
    {
        var result = await service.GetCourseReviewsAsync(courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("user/{userId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUserReviewsAsync(int userId, CancellationToken ct)
    {
        var result = await service.GetAllUserReviewsAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
