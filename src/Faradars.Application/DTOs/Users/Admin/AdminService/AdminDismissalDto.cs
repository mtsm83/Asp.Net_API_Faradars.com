namespace Faradars.Application.DTOs.Users.Admin.AdminService;

public class AdminDismissalDto
{
    public int DismissalId { get; set; }
    public int AdminId { get; set; }
    public string? DismissalReason { get; set; }
    public int DismissalTypeId { get; set; }
}