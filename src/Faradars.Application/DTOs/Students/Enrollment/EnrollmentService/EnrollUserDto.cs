namespace Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;

public class EnrollUserDto
{
    public int UserId { get; init; }
    public int CourseId { get; init; }
    public int? PaymentId { get; init; }
}