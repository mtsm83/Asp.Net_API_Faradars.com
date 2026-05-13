using Asp.Versioning;
using Faradars.Application.DTOs.Users.Teacher.TeacherService;
using Faradars.Application.Interfaces.Services.Users.Teacher;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.User_v1.Users.Teacher;

[ApiVersion("1")]
public class TeacherController(ITeacherService service) : AdminBaseController
{
    [HttpPost("info")]
    public async Task<IActionResult> AddTeacherInfoAsync([FromBody] AddTeacherDto dto, CancellationToken ct)

    {
        var result = await service.AddTeacherInfoAsync(dto, ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut("info")]
    public async Task<IActionResult> UpdateTeacherInfoAsync([FromBody] UpdateTeacherDto dto, CancellationToken ct)

    {
        var result = await service.UpdateTeacherInfoAsync(dto, ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("info/{teacherId:int}")]
    public async Task<IActionResult> DeleteTeacherInfoAsync(int teacherId, CancellationToken ct)

    {
        var result = await service.DeleteTeacherInfoAsync(teacherId, ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok();
    }

    [HttpGet("info/{teacherId:int}")]
    public async Task<IActionResult> GetFullTeacherInfoByIdAsync(int teacherId, CancellationToken ct)

    {
        var result = await service.GetFullTeacherInfoByIdAsync(teacherId, ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("info/course/{courseId:int}")]
    public async Task<IActionResult> GetFullTeacherInfoByCourseIdAsync(int courseId, CancellationToken ct)

    {
        var result = await service.GetFullTeacherInfoByCourseIdAsync(courseId, ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("info/all")]
    public async Task<IActionResult> GetAllTeachersInfoAsync(CancellationToken ct)

    {
        var result = await service.GetAllTeachersInfoAsync(ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("dismissal")]
    public async Task<IActionResult> AddTeacherDismissalAsync([FromBody] DismissTeacherDto dto, CancellationToken ct)

    {
        var result = await service.AddTeacherDismissalAsync(dto, ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("dismissal/{dismissalId:int}")]
    public async Task<IActionResult> DeleteTeacherDismissalAsync(int dismissalId, CancellationToken ct)

    {
        var result = await service.DeleteTeacherDismissalAsync(dismissalId, ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok();
    }

    [HttpGet("dismissal/{dismissalId:int}")]
    public async Task<IActionResult> GetTeacherDismissalByIdAsync(int dismissalId, CancellationToken ct)

    {
        var result = await service.GetTeacherDismissalByIdAsync(dismissalId, ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("dismissal/all")]
    public async Task<IActionResult> GetAllTeacherDismissalsAsync(CancellationToken ct)

    {
        var result = await service.GetAllTeacherDismissalsAsync(ct);

        if (result.IsFailure)

            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}