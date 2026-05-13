using Asp.Versioning;
using Faradars.Application.DTOs.Management.Request.RequestSubjectService;
using Faradars.Application.Interfaces.Services.Management.Request;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Management.Request;

[ApiVersion("1")]
public class RequestSubjectController(IRequestSubjectService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] AddRequestSubjectDto dto, CancellationToken ct)
    {
        var result = await service.CreateAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateRequestSubjectDto dto, CancellationToken ct)
    {
        var result = await service.UpdateAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{subjectId:int}")]
    public async Task<IActionResult> DeleteAsync(int subjectId, CancellationToken ct)
    {
        var result = await service.DeleteAsync(subjectId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{subjectId:int}")]
    public async Task<IActionResult> GetByIdAsync(int subjectId, CancellationToken ct)
    {
        var result = await service.GetByIdAsync(subjectId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken ct)
    {
        var result = await service.GetAllAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
