using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Management.Request.Refund;

namespace Faradars.Domain.Entities.Students.Enrollment;

public class EnrollmentWithdrawal : BaseEntity
{
    public int EnrollmentId { get; set; }
    public int RefundRequestId { get; set; }
    public string WithdrawalReason { get; set; } = null!; // admin must fill, Equal to request reason

    public Enrollment Enrollment { get; set; } = null!;
    public RefundRequest RefundRequest { get; set; } = null!;
}