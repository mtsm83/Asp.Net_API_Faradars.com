using Asp.Versioning;
using Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;
using Faradars.Application.Interfaces.Services.Students.Enrollment;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Students.Enrollment;

[ApiVersion("1")]
public class DismissalTypeController(IDismissalTypeService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddDismissalTypeAsync([FromBody] AddDismissalTypeDto dto, CancellationToken ct)
    {
        var result = await service.AddDismissalTypeAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{dismissalId:int}")]
    public async Task<IActionResult> DeleteDismissalTypeAsync(int dismissalId, CancellationToken ct)
    {
        var result = await service.DeleteDismissalTypeAsync(dismissalId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        // Assuming Unit is an empty struct or similar, you might return NoContent() or Ok()
        // depending on your API design for successful deletion.
        return Ok(result.Value);
    }

    [HttpGet("{dismissalId:int}")]
    public async Task<IActionResult> GetDismissalTypeByIdAsync(int dismissalId, CancellationToken ct)
    {
        var result = await service.GetDismissalTypeByIdAsync(dismissalId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDismissalTypesAsync(CancellationToken ct)
    {
        var result = await service.GetAllDismissalTypesAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
