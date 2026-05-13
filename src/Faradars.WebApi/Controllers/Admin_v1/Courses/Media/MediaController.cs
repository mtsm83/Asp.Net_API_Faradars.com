using Asp.Versioning;
using Faradars.Application.Interfaces.Services.Courses.Media;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Courses.Media;

[ApiVersion("1")]
public class MediaController(IMediaService service) : AdminBaseController
{
    [HttpPost("upload")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UploadAssetFileAsync(IFormFile file, CancellationToken ct)
    {
        var result = await service.UploadAssetFileAsync(file, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{fileId:int?}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAssetFileAsync(int? fileId, CancellationToken ct)
    {
        var result = await service.DeleteAssetFileAsync(fileId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("{fileId:int?}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAssetFileByIdAsync(int? fileId, CancellationToken ct)
    {
        var result = await service.GetAssetFileByIdAsync(fileId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpGet("download")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DownloadAssetFileAsync([FromQuery] string fileUrl, CancellationToken ct)
    {
        var result = await service.DownloadAssetFileAsync(fileUrl, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
