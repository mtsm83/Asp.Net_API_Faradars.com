using Faradars.Application.DTOs.Users.Role.RoleService;

namespace Faradars.Application.Mappers.Users.Role;

public static class RoleServiceMappers
{
    public static void MapAddRoleDto(this Domain.Entities.Users.Role.Role role, CreateRoleDto dto)
    {
        role.Name = dto.Name;
        role.Description = dto.Description;
    }

    public static void MapUpdateRoleDto(this Domain.Entities.Users.Role.Role role, UpdateRoleDto dto)
    {
        role.Name = dto.Name ?? role.Name;
        role.Description = dto.Description ??  role.Description;
    }

    public static RoleDto MapToRoleDto(this Domain.Entities.Users.Role.Role role)
    {
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description
        };
    }
}