namespace Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;

public class DismissEnrollmentDto
{
    public int EnrollmentId { get; set; }
    public int DismissalTypeId { get; set; }
    public string DismissalReason { get; set; } = null!;
}