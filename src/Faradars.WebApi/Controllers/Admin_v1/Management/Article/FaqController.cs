using Asp.Versioning;
using Faradars.Application.DTOs.Management.Article.FaqService;
using Faradars.Application.Interfaces.Services.Management.Article;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Management.Article;

[ApiVersion("1")]
public class FaqController(IFaqService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddFaqAsync([FromBody] AddFaqDto dto, CancellationToken ct)
    {
        var result = await service.AddFaqAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateFaqAsync([FromBody] UpdateFaqDto dto, CancellationToken ct)
    {
        var result = await service.UpdateFaqAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{faqId:int}")]
    public async Task<IActionResult> DeleteFaqAsync(int faqId, CancellationToken ct)
    {
        var result = await service.DeleteFaqAsync(faqId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{faqId:int}")]
    public async Task<IActionResult> GetFaqByIdAsync(int faqId, CancellationToken ct)
    {
        var result = await service.GetFaqByIdAsync(faqId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllFaqsAsync(CancellationToken ct)
    {
        var result = await service.GetAllFaqsAsync(ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
