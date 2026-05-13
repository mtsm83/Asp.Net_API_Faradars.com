namespace Faradars.Application.DTOs.Users.Teacher.TeacherService;

public class DismissTeacherDto
{
    public int TeacherId { get; set; }
    public string? DismissalReason { get; set; }
    public int DismissalTypeId { get; set; }
}