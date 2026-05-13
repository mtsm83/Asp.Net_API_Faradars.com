using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Tag.TagService;
using Faradars.Application.Interfaces.Services.Courses.Tag;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Tag;

[ApiVersion("1")]
public class TagController(ITagService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddTagAsync([FromBody] AddTagDto dto, CancellationToken ct)
    {
        var result = await service.AddTagAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTagAsync([FromBody] UpdateTagDto dto, CancellationToken ct)
    {
        var result = await service.UpdateTagAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{tagId:int}")]
    public async Task<IActionResult> DeleteTagAsync(int tagId, CancellationToken ct)
    {
        var result = await service.DeleteTagAsync(tagId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPost("{tagId:int}/courses/{courseId:int}/assign")]
    public async Task<IActionResult> AssignCourseToTagAsync(int courseId, int tagId, CancellationToken ct)
    {
        var result = await service.AssignCourseToTagAsync(courseId, tagId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{tagId:int}/courses/{courseId:int}/remove")]
    public async Task<IActionResult> RemoveCourseThanTagAsync(int courseId, int tagId, CancellationToken ct)
    {
        var result = await service.RemoveCourseThanTagAsync(courseId, tagId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTagByIdAsync(int id, CancellationToken ct)
    {
        var result = await service.GetTagByIdAsync(id, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllTagsAsync(CancellationToken ct)
    {
        var result = await service.GetAllTagsAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("courses/{courseId:int}")]
    public async Task<IActionResult> GetTagsByCourseIdAsync(int courseId, CancellationToken ct)
    {
        var result = await service.GetTagsByCourseIdAsync(courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
