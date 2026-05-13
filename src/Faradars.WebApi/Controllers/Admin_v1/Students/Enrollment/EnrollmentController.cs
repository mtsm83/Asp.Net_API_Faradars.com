using Asp.Versioning;
using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Application.Interfaces.Services.Students.Enrollment;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Students.Enrollment;

[ApiVersion("1")]
public class EnrollmentController(IEnrollmentService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> EnrollUserAsync([FromBody] EnrollUserDto dto, CancellationToken ct)
    {
        var result = await service.EnrollUserAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{enrollmentId:int}")]
    public async Task<IActionResult> DeleteEnrollmentRecordAsync(int enrollmentId, CancellationToken ct)
    {
        var result = await service.DeleteEnrollmentRecordAsync(enrollmentId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        // You can choose Ok(result.Value) or NoContent() — following your project's convention:
        return Ok(result.Value);
    }

    [HttpPost("has-access")]
    public async Task<IActionResult> HasAccessAsync([FromBody] CheckAccessDto dto, CancellationToken ct)
    {
        var result = await service.HasAccessAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetUserEnrollmentsAsync(int userId, CancellationToken ct)
    {
        var result = await service.GetUserEnrollmentsAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
