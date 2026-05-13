using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Content.LessonService;
using Faradars.Application.Interfaces.Services.Courses.Content;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Content;

[ApiVersion("1")]
public class LessonController(ILessonService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddLessonAsync([FromBody] AddLessonDto dto, CancellationToken ct)
    {
        var result = await service.AddLessonAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("add-asset-file")]
    public async Task<IActionResult> AddAssetFileToLessonAsync([FromBody]AddAssetFileDto dto, CancellationToken ct)
    {
        var result = await service.AddAssetFileToLessonAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{lessonId:int}/asset-file/{fileId:int}")]
    public async Task<IActionResult> DeleteAssetFileThanLessonAsync(int fileId, int lessonId, CancellationToken ct)
    {
        var result = await service.DeleteAssetFileThanLessonAsync(fileId, lessonId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateLessonAsync([FromBody] UpdateLessonDto dto, CancellationToken ct)
    {
        var result = await service.UpdateLessonAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{lessonId:int}")]
    public async Task<IActionResult> DeleteLessonAsync(int lessonId, CancellationToken ct)
    {
        var result = await service.DeleteLessonAsync(lessonId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("section/{sectionId:int}")]
    public async Task<IActionResult> GetAllSectionLessonsAsync(int sectionId, CancellationToken ct)
    {
        var result = await service.GetAllSectionLessonsAsync(sectionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{lessonId:int}")]
    public async Task<IActionResult> GetLessonByIdAsync(int lessonId, CancellationToken ct)
    {
        var result = await service.GetLessonByIdAsync(lessonId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut("mark-complete")]
    public async Task<IActionResult> MarkLessonCompleteAsync([FromBody] MarkCompleteDto dto, CancellationToken ct)
    {
        var result = await service.MarkLessonCompleteAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
