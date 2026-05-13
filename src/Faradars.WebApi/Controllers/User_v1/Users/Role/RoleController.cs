using Asp.Versioning;
// Assuming UserDto is here
using Faradars.Application.DTOs.Users.Role.RoleService;
using Faradars.Application.Interfaces.Services.Users.Role;
using Faradars.WebApi.Common;
using Microsoft.AspNetCore.Mvc;

namespace Faradars.WebApi.Controllers.User_v1.Users.Role;

[ApiVersion("1")]
public class RoleController(IRoleService service) : UserBaseController
{
    [HttpPost("add")] 
    public async Task<IActionResult> AddRoleAsync([FromBody] CreateRoleDto dto, CancellationToken ct)
    {
        var result = await service.AddRoleAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }

    [HttpDelete("delete/{roleId:int}")] // Changed route to avoid multiple DELETEs on the same path and to be more specific
    public async Task<IActionResult> DeleteRoleAsync(int roleId, CancellationToken ct)
    {
        var result = await service.DeleteRoleAsync(roleId, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(); // Typically, delete operations return Ok() with no content on success
    }

    [HttpGet("{roleId:int}")] // Changed route for clarity
    public async Task<IActionResult> GetRoleByIdAsync(int roleId, CancellationToken ct)
    {
        var result = await service.GetRoleByIdAsync(roleId, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }

    [HttpPut] // Assuming update uses PUT
    public async Task<IActionResult> UpdateRoleAsync([FromBody] UpdateRoleDto dto, CancellationToken ct)
    {
        var result = await service.UpdateRoleAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }

    [HttpGet("all")] // Route for getting all roles
    public async Task<IActionResult> GetAllRolesAsync(CancellationToken ct)
    {
        var result = await service.GetAllRolesAsync(ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }

    [HttpPost("assign-user-role")]
    public async Task<IActionResult> AssignRoleToUserAsync([FromBody] AssignUserRoleDto dto, CancellationToken ct) // Assuming a DTO for this or separate params
    {
        var result = await service.AssignRoleToUserAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }

    [HttpDelete("remove-user-role")]
    public async Task<IActionResult> RemoveRoleThanUserAsync([FromBody] RemoveUserRoleDto dto, CancellationToken ct) // Assuming a DTO for this or separate params
    {
        var result = await service.RemoveRoleThanUserAsync(dto, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok();
    }

    [HttpGet("user-roles/{userId:int}")]
    public async Task<IActionResult> GetAllUserRolesAsync(int userId, CancellationToken ct)
    {
        var result = await service.GetAllUserRolesAsync(userId, ct);

        if (result.IsFailure)
            return BadRequest(new
            {
                error = result.Error.Code,
                message = result.Error.Message
            });

        return Ok(result.Value);
    }
}
