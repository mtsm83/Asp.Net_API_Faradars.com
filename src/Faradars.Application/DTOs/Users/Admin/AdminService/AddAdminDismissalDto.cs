namespace Faradars.Application.DTOs.Users.Admin.AdminService;

public class AddAdminDismissalDto
{
    public int AdminId { get; set; }
    public string? DismissalReason { get; set; }
    public int DismissalTypeId { get; set; }
}