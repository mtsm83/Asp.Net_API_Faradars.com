using Asp.Versioning;
using Faradars.Application.DTOs.Users.Information.UserService;
using Faradars.Application.Interfaces.Services.Users.Information;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.User_v1.Users.Information;

[ApiVersion("1")]
public class UserController : UserBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetUserInfoAsync(int userId, CancellationToken ct)
    {
        var result = await _userService.GetUserByIdAsync(userId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }
    
    [HttpGet("current-user")]
    public async Task<IActionResult> GetCurrentUserAsync(CancellationToken ct)
    {
        var result = await _userService.GetCurrentUserAsync(ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpGet("all/{userId:int}")]
    public async Task<IActionResult> GetAllUsersInfoAsync(CancellationToken ct)
    {
        var result = await _userService.GetAllUsersInfoAsync(ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpGet("search/{searchText}")]
    public async Task<IActionResult> GetAllUsersByNameSearchAsync([FromRoute] string searchText, CancellationToken ct)
    {
        var result = await _userService.GetAllUsersByNameSearchAsync(searchText, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpGet("course/{courseId:int}")]
    public async Task<IActionResult> GetAllStudentsOfCourseAsync(int courseId, CancellationToken ct)
    {
        var result = await _userService.GetAllStudentsOfCourseAsync(courseId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserInfoAsync([FromBody] UpdateUserInfoDto dto, CancellationToken ct)
    {
        var result = await _userService.UpdateUserInfoAsync(dto, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> DeleteUserInfoAsync(int userId, CancellationToken ct)
    {
        var result = await _userService.DeleteUserInfoAsync(userId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok();
    }

    [HttpPost("profile/{userId:int}")]
    public async Task<IActionResult> AddUserProfileAsync(AddUserProfileDto dto, CancellationToken ct)
    {
        var result = await _userService.AddUserProfileAsync(dto, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok(result.Value);
    }

    [HttpDelete("profile/{userId:int}")]
    public async Task<IActionResult> RemoveUserProfileAsync(int userId, CancellationToken ct)
    {
        var result = await _userService.RemoveUserProfileAsync(userId, ct);
        if (result.IsFailure)
            return BadRequest(new { error = result.Error.Code, message = result.Error.Message });
        return Ok();
    }
}
