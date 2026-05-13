namespace Faradars.Application.DTOs.Users.Admin.AdminService;

public class UpdateAdminDto
{
    public int AdminId { get; set; }
    public string? Description { get; set; }
    public string? Bio { get; set; }
    public string? LinkedinUrl { get; set; }
}