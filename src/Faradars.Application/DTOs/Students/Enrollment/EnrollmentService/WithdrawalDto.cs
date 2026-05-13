namespace Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;

public class WithdrawalDto
{
    public int WithdrawalId { get; set; }
    public int EnrollmentId { get; set; }
    public int RefundRequestId { get; set; }
    public int CreatorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string WithdrawalReason { get; set; } = null!;
}