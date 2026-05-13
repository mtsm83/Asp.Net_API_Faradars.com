using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Pricing.CoursePriceService;
using Faradars.Application.Interfaces.Services.Courses.Pricing;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Pricing;

[ApiVersion("1")]
public class CoursePriceController(ICoursePriceService service) : AdminBaseController
{
    [HttpPost("add")]
    public async Task<IActionResult> AddPriceToCourseAsync([FromBody] AddPriceDto dto, CancellationToken ct)
    {
        var result = await service.AddPriceToCourseAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdatePriceOfCourseAsync([FromBody] UpdatePriceDto dto, CancellationToken ct)
    {
        var result = await service.UpdatePriceOfCourseAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{courseId:int}")]
    public async Task<IActionResult> DeleteCoursePriceAsync(int courseId, CancellationToken ct)
    {
        var result = await service.DeleteCoursePriceAsync(courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("price/{coursePriceId:int}")]
    public async Task<IActionResult> DeleteCoursePriceByIdAsync(int coursePriceId, CancellationToken ct)
    {
        var result = await service.DeleteCoursePriceByIdAsync(coursePriceId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{courseId:int}")]
    public async Task<IActionResult> GetCoursePriceByIdAsync(int courseId, CancellationToken ct)
    {
        var result = await service.GetCoursePriceByIdAsync(courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
