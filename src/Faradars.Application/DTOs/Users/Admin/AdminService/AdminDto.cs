namespace Faradars.Application.DTOs.Users.Admin.AdminService;

public class AdminDto
{
    public int AdminId { get; set; }
    public int PromoterId { get; set; }
    public string? Description { get; set; }
    public string? Bio { get; set; }
    public string? LinkedinUrl { get; set; }
    public DateTime PromotedAt { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
}