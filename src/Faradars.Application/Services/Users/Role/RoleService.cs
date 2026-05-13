using Faradars.Application.DTOs.Users.Information.UserService;
using Faradars.Application.DTOs.Users.Role.RoleService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Users.Role;
using Faradars.Application.Mappers.Users.Information;
using Faradars.Application.Mappers.Users.Role;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Entities.Users.Role;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Users.Role;

public class RoleService(
    IRepository<Domain.Entities.Users.Role.Role> roleRepository,
    IRepository<UserRole> userRoleRepository,
    IRepository<User> userRepository,
    IJwtTokenManager jwtTokenManager,
    IUserContextService userContextService) : IRoleService, IScopedDependency
{
    public async Task<Result<RoleDto>> AddRoleAsync(CreateRoleDto dto, CancellationToken ct)
    {
        var newRole = new Domain.Entities.Users.Role.Role();
        newRole.MapAddRoleDto(dto);
        newRole.CreatedBy = userContextService.CurrentUser.UserId;
        await roleRepository.AddAsync(newRole, ct);
        var roleDto = newRole.MapToRoleDto();
        return Result.Success(roleDto);
    }

    public async Task<Result<RoleDto>> UpdateRoleAsync(UpdateRoleDto dto, CancellationToken ct)
    {
        var role = await roleRepository.GetByIdAsync(ct, dto.Id);
        if (role == null)
            return Result.Failure<RoleDto>(Error.NotFound);
        role.MapUpdateRoleDto(dto);
        role.UpdatedAt = DateTime.Now;
        role.UpdatedBy = userContextService.CurrentUser.UserId;
        await roleRepository.UpdateAsync(role, ct);
        var roleDto = role.MapToRoleDto();
        return Result.Success(roleDto);
    }

    public async Task<Result<RoleDto>> DeleteRoleAsync(int roleId, CancellationToken ct)
    {
        // todo: what about ppl who got the role?

        var role = await roleRepository.GetByIdAsync(ct, roleId);
        if (role == null)
            return Result.Failure<RoleDto>(Error.NotFound);
        await roleRepository.DeleteAsync(role, ct);
        var roleDto = role.MapToRoleDto();
        return Result.Success(roleDto);
    }

    public async Task<Result<RoleDto>> GetRoleByIdAsync(int roleId, CancellationToken ct)
    {
        var role = await roleRepository.GetByIdAsync(ct, roleId);
        if (role == null)
            return Result.Failure<RoleDto>(Error.NotFound);

        var roleDto = role.MapToRoleDto();
        return Result.Success(roleDto);
    }

    public async Task<Result<List<RoleDto>>> GetAllRolesAsync(CancellationToken ct)
    {
        var roles = await roleRepository.TableNoTracking.ToListAsync(ct);
        if (roles.Count == 0)
            return Result.Failure<List<RoleDto>>(Error.NotFound);
        var rolesDtos = roles.Select(x => x.MapToRoleDto()).ToList();
        return Result.Success(rolesDtos);
    }

    public async Task<Result<UserDto>> AssignRoleToUserAsync(AssignUserRoleDto dto, CancellationToken ct)
    {
        var role = await roleRepository.GetByIdAsync(ct, dto.RoleId);
        if (role == null)
            return Result.Failure<UserDto>(Error.NotFound);

        var user = await userRepository.GetByIdAsync(ct, dto.UserId);
        if (user == null)
            return Result.Failure<UserDto>(Error.NotFound);

        var newUserRole = new UserRole
        {
            RoleId = dto.RoleId,
            UserId = dto.UserId,
            CreatedBy = userContextService.CurrentUser.UserId
        };
        await userRoleRepository.AddAsync(newUserRole, ct);
        var userDto = user.MapToUserDto();
        return Result.Success(userDto);
    }

    public async Task<Result<UserDto>> RemoveRoleThanUserAsync(RemoveUserRoleDto dto, CancellationToken ct)
    {
        var userRole = await userRoleRepository.TableNoTracking
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.UserId == dto.UserId && x.RoleId == dto.RoleId, ct);
        if (userRole == null)
            return Result.Failure<UserDto>(Error.NotFound);
        var userDto = userRole.User.MapToUserDto();
        await userRoleRepository.DeleteAsync(userRole, ct);
        return Result.Success(userDto);
    }

    public async Task<Result<List<RoleDto>>> GetAllUserRolesAsync(int userId, CancellationToken ct)
    {
        var userRoles = await userRoleRepository.TableNoTracking
            .Where(x => x.UserId == userId).Select(ur => ur.Role).ToListAsync(ct);

        if (userRoles.Count == 0)
            return Result.Failure<List<RoleDto>>(Error.NotFound);

        var userRolesDtos = userRoles.Select(x => x.MapToRoleDto()).ToList();
        return Result.Success(userRolesDtos);
    }
}