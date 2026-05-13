namespace Faradars.Application.DTOs.Users.Teacher.TeacherService;

public class TeacherDismissalDto
{
    public int DismissalId { get; set; }
    public int TeacherId { get; set; }
    public string? DismissalReason { get; set; }
    public int DismissalTypeId { get; set; }
    
    public int CreatorId { get; set; }
    public int? UpdaterId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}