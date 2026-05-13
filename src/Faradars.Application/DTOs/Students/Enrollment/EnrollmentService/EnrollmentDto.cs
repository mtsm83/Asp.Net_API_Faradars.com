namespace Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;

public class EnrollmentDto
{
    public int EnrollmentId { get; init; }
    public int StudentId { get; init; }
    public int CourseId { get; init; }
    public DateTime EnrolledAt { get; init; }
}