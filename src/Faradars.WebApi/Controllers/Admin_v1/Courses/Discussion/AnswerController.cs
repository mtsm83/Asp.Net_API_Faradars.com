using Asp.Versioning;
using Faradars.Application.DTOs.Courses.Discussion.AnswerService;
using Faradars.Application.Interfaces.Services.Courses.Discussion;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Discussion;

[ApiVersion("1")]
public class AnswerController(IAnswerService service) : AdminBaseController
{
    [HttpPost]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddAnswerAsync([FromBody] AddAnswerDto dto, CancellationToken ct)
    {
        var result = await service.AddAnswerAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAnswerAsync([FromBody] UpdateAnswerDto dto, CancellationToken ct)
    {
        var result = await service.UpdateAnswerAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{answerId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAnswerAsync(int answerId, CancellationToken ct)
    {
        var result = await service.DeleteAnswerAsync(answerId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("question/{questionId:int}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllQuestionAnswersAsync(int questionId, CancellationToken ct)
    {
        var result = await service.GetAllQuestionAnswersAsync(questionId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
