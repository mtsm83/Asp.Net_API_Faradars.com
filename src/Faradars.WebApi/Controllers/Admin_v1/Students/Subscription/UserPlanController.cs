using Asp.Versioning;
using Faradars.Application.DTOs.Students.Subscription.UserPlanService;
using Faradars.Application.Interfaces.Services.Students.Subscription;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.Admin_v1.Students.Subscription;

[ApiVersion("1")]
public class UserPlanController(IUserPlanService service) : AdminBaseController
{
    [HttpPost]
    public async Task<IActionResult> AddUserPlanAsync([FromBody] AddUserPlanDto dto, CancellationToken ct)
    {
        var result = await service.AddUserPlanAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserPlanAsync([FromBody] UpdateUserPlanDto dto, CancellationToken ct)
    {
        var result = await service.UpdateUserPlanAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }

    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> DeleteUserPlanAsync(int userId, CancellationToken ct)
    {
        var result = await service.DeleteUserPlanAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok();
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetAllUserPlansAsync(int userId, CancellationToken ct)
    {
        var result = await service.GetAllUserPlansAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });

        return Ok(result.Value);
    }
}
