using Faradars.Application.DTOs.Users.Information.UserService;
using Faradars.Application.DTOs.Users.Role.RoleService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Users.Role;

public interface IRoleService
{
    Task<Result<RoleDto>> AddRoleAsync(CreateRoleDto dto, CancellationToken ct);
    Task<Result<RoleDto>> UpdateRoleAsync(UpdateRoleDto dto, CancellationToken ct);
    Task<Result<RoleDto>> DeleteRoleAsync(int roleId, CancellationToken ct);
    Task<Result<RoleDto>> GetRoleByIdAsync(int roleId, CancellationToken ct);
    Task<Result<List<RoleDto>>> GetAllRolesAsync(CancellationToken ct);

    Task<Result<UserDto>> AssignRoleToUserAsync(AssignUserRoleDto dto, CancellationToken ct);
    Task<Result<UserDto>> RemoveRoleThanUserAsync(RemoveUserRoleDto dto, CancellationToken ct);
    
    Task<Result<List<RoleDto>>> GetAllUserRolesAsync(int userId, CancellationToken ct); // important API
    
}