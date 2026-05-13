using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Content.SectionService;
using Faradars.Application.Interfaces.Services.Courses.Content;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Content;

[ApiVersion("1")]
public class SectionController(ISectionService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddSectionAsync([FromBody] AddSectionDto dto, CancellationToken ct)
    {
        var result = await service.AddSectionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSectionAsync([FromBody] UpdateSectionDto dto, CancellationToken ct)
    {
        var result = await service.UpdateSectionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{sectionId:int}")]
    public async Task<IActionResult> DeleteSectionAsync(int sectionId, CancellationToken ct)
    {
        var result = await service.DeleteSectionAsync(sectionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("course/{courseId:int}")]
    public async Task<IActionResult> GetCourseSectionsAsync(int courseId, CancellationToken ct)
    {
        var result = await service.GetCourseSectionsAsync(courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}