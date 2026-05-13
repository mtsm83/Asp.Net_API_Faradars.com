using Asp.Versioning;
using Faradars.Application.DTOs.Management.Request.RefundRequestService;
using Faradars.Application.Interfaces.Services.Management.Request;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Management.Request;

[ApiVersion("1")]
public class RefundRequestController(IRefundRequestService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] AddRefundRequestDto dto, CancellationToken ct)
    {
        var result = await service.CreateAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateRefundRequestDto dto, CancellationToken ct)
    {
        var result = await service.UpdateAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{refundRequestId:int}")]
    public async Task<IActionResult> DeleteAsync(int refundRequestId, CancellationToken ct)
    {
        var result = await service.DeleteAsync(refundRequestId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{refundRequestId:int}")]
    public async Task<IActionResult> GetByIdAsync(int refundRequestId, CancellationToken ct)
    {
        var result = await service.GetByIdAsync(refundRequestId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct)
    {
        var result = await service.GetAllAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetByCurrentUserAsync(CancellationToken ct)
    {
        var result = await service.GetByCurrentUserAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
