using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Pricing.BundleService;
using Faradars.Application.Interfaces.Services.Courses.Pricing;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Pricing;

[ApiVersion("1")]
public class BundleController(IBundleService service) : AdminBaseController
{
    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddBundleAsync([FromBody] AddBundleDto dto, CancellationToken ct)
    {
        var result = await service.AddBundleAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBundleAsync([FromBody] UpdateBundleDto dto, CancellationToken ct)
    {
        var result = await service.UpdateBundleAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{bundleId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBundleAsync(int bundleId, CancellationToken ct)
    {
        var result = await service.DeleteBundleAsync(bundleId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("{bundle:int}/banner/{bannerId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddBannerImageToBundleAsync(int bundle, int bannerId, CancellationToken ct)
    {
        var result = await service.AddBannerImageToBundleAsync(bundle, bannerId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("{bundle:int}/course/{courseId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignCourseToBundleAsync(int bundle, int courseId, CancellationToken ct)
    {
        var result = await service.AssignCourseToBundleAsync(bundle, courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{bundle:int}/course/{courseId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveCourseThanBundleAsync(int bundle, int courseId, CancellationToken ct)
    {
        var result = await service.RemoveCourseThanBundleAsync(bundle, courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{bundleId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetBundleByIdAsync(int bundleId, CancellationToken ct)
    {
        var result = await service.GetBundleByIdAsync(bundleId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllBundlesAsync(CancellationToken ct)
    {
        var result = await service.GetAllBundlesAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
