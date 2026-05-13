using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Pricing.DiscountService;
using Faradars.Application.Interfaces.Services.Courses.Pricing;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Pricing;

[ApiVersion("1")]
public class DiscountController(IDiscountService service) : AdminBaseController
{
    [HttpPost("coupons/add")]
    public async Task<IActionResult> AddCouponAsync([FromBody] AddCouponDto dto, CancellationToken ct)
    {
        var result = await service.AddCouponAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut("coupons/update")]
    public async Task<IActionResult> UpdateCouponAsync([FromBody] UpdateCouponDto dto, CancellationToken ct)
    {
        var result = await service.UpdateCouponAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("coupons/{couponId:int}")]
    public async Task<IActionResult> DeleteCouponAsync(int couponId, CancellationToken ct)
    {
        var result = await service.DeleteCouponAsync(couponId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("coupons/{couponId:int}")]
    public async Task<IActionResult> GetCouponByIdAsync(int couponId, CancellationToken ct)
    {
        var result = await service.GetCouponByIdAsync(couponId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("coupons/all")]
    public async Task<IActionResult> GetAllCouponsAsync(CancellationToken ct)
    {
        var result = await service.GetAllCouponsAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("usages/add")]
    public async Task<IActionResult> AddCouponUsageAsync([FromBody] AddCouponUsageDto dto, CancellationToken ct)
    {
        var result = await service.AddCouponUsageAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("usages/{usageId:int}")]
    public async Task<IActionResult> DeleteCouponUsageAsync(int usageId, CancellationToken ct)
    {
        var result = await service.DeleteCouponUsageAsync(usageId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        // For Delete operations returning Unit, Ok(result.Value) is used to be consistent with other delete methods.
        // If Unit signifies no value returned, NoContent() could be an alternative, but following the established pattern.
        return Ok(result.Value);
    }

    [HttpGet("usages/{usageId:int}")]
    public async Task<IActionResult> GetCouponUsageByIdAsync(int usageId, CancellationToken ct)
    {
        var result = await service.GetCouponUsageByIdAsync(usageId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("usages/all")]
    public async Task<IActionResult> GetAllUsagesAsync(CancellationToken ct)
    {
        var result = await service.GetAllUsagesAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
