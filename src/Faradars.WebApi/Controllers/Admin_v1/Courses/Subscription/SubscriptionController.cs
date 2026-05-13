using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Subsctiption.SubscriptionService;
using Faradars.Application.Interfaces.Services.Courses.Subscription;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Subscription;

[ApiVersion("1")]
public class SubscriptionController(ISubscriptionService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddSubscriptionAsync([FromBody] AddSubscriptionDto dto, CancellationToken ct)
    {
        var result = await service.AddSubscriptionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSubscriptionAsync([FromBody] UpdateSubscriptionDto dto, CancellationToken ct)
    {
        var result = await service.UpdateSubscriptionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{subscriptionId:int}")]
    public async Task<IActionResult> DeleteSubscriptionAsync(int subscriptionId, CancellationToken ct)
    {
        var result = await service.DeleteSubscriptionAsync(subscriptionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
    
    [HttpGet("{subscriptionId:int}")]
    public async Task<IActionResult> GetSubscriptionByIdAsync(int subscriptionId, CancellationToken ct)
    {
        var result = await service.GetSubscriptionByIdAsync(subscriptionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllSubscriptionsAsync(CancellationToken ct)
    {
        var result = await service.GetAllSubscriptionsAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("{subscriptionId:int}/courses/{courseId:int}/assign")]
    public async Task<IActionResult> AssignCourseToSubscriptionAsync(int subscriptionId, int courseId, CancellationToken ct)
    {
        var result = await service.AssignCourseToSubscriptionAsync(subscriptionId, courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{subscriptionId:int}/courses/{courseId:int}/remove")]
    public async Task<IActionResult> RemoveCourseThanSubscriptionAsync(int subscriptionId, int courseId, CancellationToken ct)
    {
        var result = await service.RemoveCourseThanSubscriptionAsync(subscriptionId, courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("{subscriptionId:int}/tags/{tagId:int}/assign")]
    public async Task<IActionResult> AssignTagToSubscriptionAsync(int subscriptionId, int tagId, CancellationToken ct)
    {
        var result = await service.AssignTagToSubscriptionAsync(subscriptionId, tagId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{subscriptionId:int}/tags/{tagId:int}/remove")]
    public async Task<IActionResult> RemoveTagThanSubscriptionAsync(int subscriptionId, int tagId, CancellationToken ct)
    {
        var result = await service.RemoveTagThanSubscriptionAsync(subscriptionId, tagId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("{subscriptionId:int}/categories/{categoryId:int}/assign")]
    public async Task<IActionResult> AssignCategoryToSubscriptionAsync(int subscriptionId, int categoryId, CancellationToken ct)
    {
        var result = await service.AssignCategoryToSubscriptionAsync(subscriptionId, categoryId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{subscriptionId:int}/categories/{categoryId:int}/remove")]
    public async Task<IActionResult> RemoveCategoryThanSubscriptionAsync(int subscriptionId, int categoryId, CancellationToken ct)
    {
        var result = await service.RemoveCategoryThanSubscriptionAsync(subscriptionId, categoryId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
