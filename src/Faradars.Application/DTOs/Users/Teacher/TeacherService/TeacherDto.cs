namespace Faradars.Application.DTOs.Users.Teacher.TeacherService;

public class TeacherDto
{
    public int TeacherId { get; set; }
    public int UserId { get; set; }
    public string? Bio { get; set; }
    public string? LinkedinUrl { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
}