using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Pricing.OfferService;
using Faradars.Application.Interfaces.Services.Courses.Pricing;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Pricing;

[ApiVersion("1")]
public class OfferController(IOfferService service) : AdminBaseController
{
    [HttpPost("add")]
    public async Task<IActionResult> AddOfferAsync([FromBody] AddOfferDto dto, CancellationToken ct)
    {
        var result = await service.AddOfferAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateOfferAsync([FromBody] UpdateOfferDto dto, CancellationToken ct)
    {
        var result = await service.UpdateOfferAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{offerId:int}")]
    public async Task<IActionResult> DeleteOfferAsync(int offerId, CancellationToken ct)
    {
        var result = await service.DeleteOfferAsync(offerId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("{offerId:int}/courses/{courseId:int}/assign")]
    public async Task<IActionResult> AssignCourseToOfferAsync(int offerId, int courseId, CancellationToken ct)
    {
        var result = await service.AssignCourseToOfferAsync(offerId, courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{offerId:int}/courses/{courseId:int}/remove")]
    public async Task<IActionResult> RemoveCourseThanOfferAsync(int offerId, int courseId, CancellationToken ct)
    {
        var result = await service.RemoveCourseThanOfferAsync(offerId, courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{offerId:int}")]
    public async Task<IActionResult> GetOfferByIdAsync(int offerId, CancellationToken ct)
    {
        var result = await service.GetOfferByIdAsync(offerId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllOffersAsync(CancellationToken ct)
    {
        var result = await service.GetAllOffersAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
