using Asp.Versioning;
using Faradars.Application.DTOs.Payments.Gateway.GatewayService;
using Faradars.Application.Interfaces.Services.Payments.Gateway;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Payments.Gateway;

[ApiVersion("1")]
public class GatewayController(IGatewayService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddGatewayAsync([FromBody] AddGatewayDto dto, CancellationToken ct)
    {
        var result = await service.AddGatewayAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGatewayAsync([FromBody] UpdateGatewayDto dto, CancellationToken ct)
    {
        var result = await service.UpdateGatewayAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{gatewayId:int}")]
    public async Task<IActionResult> DeleteGatewayAsync(int gatewayId, CancellationToken ct)
    {
        var result = await service.DeleteGatewayAsync(gatewayId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{gatewayId:int}")]
    public async Task<IActionResult> GetGatewayByIdAsync(int gatewayId, CancellationToken ct)
    {
        var result = await service.GetGatewayByIdAsync(gatewayId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGatewaysAsync(CancellationToken ct)
    {
        var result = await service.GetAllGatewaysAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
