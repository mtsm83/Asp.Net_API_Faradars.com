using Asp.Versioning;
using Faradars.Application.DTOs.Users.Admin.AdminService;
using Faradars.Application.Interfaces.Services.Users.Admin;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.User_v1.Users.Admin;

[ApiVersion("1")]
public class AdminController : AdminBaseController
{
    private readonly IAdminService _service;

    public AdminController(IAdminService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddAdminInfoAsync([FromBody] AddAdminDto dto, CancellationToken ct)
    {
        var result = await _service.AddAdminInfoAsync(dto, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAdminInfoAsync([FromBody] UpdateAdminDto dto, CancellationToken ct)
    {
        var result = await _service.UpdateAdminInfoAsync(dto, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpDelete("{adminId:int}")]
    public async Task<IActionResult> DeleteAdminInfoAsync(int adminId, CancellationToken ct)
    {
        var result = await _service.DeleteAdminInfoAsync(adminId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok();
    }

    [HttpGet("{adminId:int}")]
    public async Task<IActionResult> GetFullAdminInfoByIdAsync(int adminId, CancellationToken ct)
    {
        var result = await _service.GetFullAdminInfoByIdAsync(adminId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdminsInfoAsync(CancellationToken ct)
    {
        var result = await _service.GetAllAdminsInfoAsync(ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpPost("dismissal")]
    public async Task<IActionResult> AddAdminDismissalAsync([FromBody] AddAdminDismissalDto dto, CancellationToken ct)
    {
        var result = await _service.AddAdminDismissalAsync(dto, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpGet("dismissal/{dismissalId:int}")]
    public async Task<IActionResult> GetAdminDismissalByIdAsync(int dismissalId, CancellationToken ct)
    {
        var result = await _service.GetAdminDismissalByIdAsync(dismissalId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpGet("dismissals")]
    public async Task<IActionResult> GetAllAdminDismissalsAsync(CancellationToken ct)
    {
        var result = await _service.GetAllAdminDismissalsAsync(ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }
}
