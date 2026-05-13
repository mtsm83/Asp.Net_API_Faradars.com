namespace Faradars.Application.DTOs.Users.Role.RoleService;

public class CreateRoleDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}