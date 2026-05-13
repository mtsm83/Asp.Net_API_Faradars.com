using Asp.Versioning;
using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Application.Interfaces.Services.Students.Enrollment;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Students.Enrollment;

[ApiVersion("1")]
public class EnrollmentWithdrawalController(IEnrollmentWithdrawalService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddWithdrawalAsync([FromBody] AddWithdrawalDto dto, CancellationToken ct)
    {
        var result = await service.AddWithdrawalAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{withdrawalId:int}")]
    public async Task<IActionResult> DeleteWithdrawalAsync(int withdrawalId, CancellationToken ct)
    {
        var result = await service.DeleteWithdrawalAsync(withdrawalId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{withdrawalId:int}")]
    public async Task<IActionResult> GetWithdrawalByIdAsync(int withdrawalId, CancellationToken ct)
    {
        var result = await service.GetWithdrawalByIdAsync(withdrawalId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetAllUserWithdrawalsAsync(int userId, CancellationToken ct)
    {
        var result = await service.GetAllUserWithdrawalsAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}