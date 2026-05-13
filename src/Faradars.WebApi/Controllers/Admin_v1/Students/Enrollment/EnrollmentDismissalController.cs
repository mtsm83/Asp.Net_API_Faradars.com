using Asp.Versioning;
using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Application.Interfaces.Services.Students.Enrollment;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Students.Enrollment;

[ApiVersion("1")]
public class EnrollmentDismissalController(IEnrollmentDismissalService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddEnrollmentDismissalAsync([FromBody] DismissEnrollmentDto dto, CancellationToken ct)
    {
        var result = await service.AddEnrollmentDismissalAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{dismissalId:int}")]
    public async Task<IActionResult> DeleteEnrollmentDismissalAsync(int dismissalId, CancellationToken ct)
    {
        var result = await service.DeleteEnrollmentDismissalAsync(dismissalId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{enrollmentId:int}")]
    public async Task<IActionResult> GetEnrollmentDismissalAsync(int enrollmentId, CancellationToken ct)
    {
        var result = await service.GetEnrollmentDismissalAsync(enrollmentId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEnrollmentDismissalAsync(CancellationToken ct)
    {
        var result = await service.GetAllEnrollmentDismissalAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}