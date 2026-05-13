using Asp.Versioning;
using Faradars.Application.DTOs.Management.Request.RequestMessageService;
using Faradars.Application.Interfaces.Services.Management.Request;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Management.Request;

[ApiVersion("1")]
public class RequestMessageController(IRequestMessageService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddRequestMessage dto, CancellationToken ct)
    {
        var result = await service.AddAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateMessageRequest dto, CancellationToken ct)
    {
        var result = await service.UpdateAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{messageId:int}")]
    public async Task<IActionResult> DeleteAsync(int messageId, CancellationToken ct)
    {
        var result = await service.DeleteAsync(messageId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{messageId:int}")]
    public async Task<IActionResult> GetByIdAsync(int messageId, CancellationToken ct)
    {
        var result = await service.GetByIdAsync(messageId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("request/{requestId:int}")]
    public async Task<IActionResult> GetByRequestIdAsync(int requestId, CancellationToken ct)
    {
        var result = await service.GetByRequestIdAsync(requestId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
