namespace Faradars.Application.DTOs.Users.Admin.AdminService;

public class AddAdminDto
{
    public int UserId { get; set; }
    public string? Description { get; set; }
    public string? Bio { get; set; }
    public string? LinkedinUrl { get; set; }
}