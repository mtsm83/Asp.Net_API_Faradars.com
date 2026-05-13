namespace Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;

public class EnrollmentDismissalDto
{
    public int Id { get; set; }
    public int EnrollmentId { get; set; }
    public int DismissalTypeId { get; set; }
    public string DismissalReason { get; set; } = null!;
    public int CreatorId { get; set; }
    public DateTime CreatedAt { get; set; }
}