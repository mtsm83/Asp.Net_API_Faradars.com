namespace Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;

public class AddWithdrawalDto
{
    public int EnrollmentId { get; set; }
    public int RefundRequestId { get; set; }
    public string WithdrawalReason { get; set; } = null!; // admin must fill, Equal to request reason
}