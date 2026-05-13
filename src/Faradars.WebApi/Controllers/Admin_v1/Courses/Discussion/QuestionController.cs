using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Discussion.QuestionService;
using Faradars.Application.Interfaces.Services.Courses.Discussion;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Discussion;

[ApiVersion("1")]
public class QuestionController(IQuestionService service) : AdminBaseController
{
    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddQuestionAsync([FromBody] AddQuestionDto dto, CancellationToken ct)
    {
        var result = await service.AddQuestionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateQuestionAsync([FromBody] UpdateQuestionDto dto, CancellationToken ct)
    {
        var result = await service.UpdateQuestionAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{questionId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteQuestionAsync(int questionId, CancellationToken ct)
    {
        var result = await service.DeleteQuestionAsync(questionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{questionId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetQuestionByIdAsync(int questionId, CancellationToken ct)
    {
        var result = await service.GetQuestionByIdAsync(questionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("course/{courseId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCourseQuestionsAsync(int courseId, CancellationToken ct)
    {
        var result = await service.GetCourseQuestionsAsync(courseId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
