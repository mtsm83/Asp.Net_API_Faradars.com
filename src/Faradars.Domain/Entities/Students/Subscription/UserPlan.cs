using System.ComponentModel.DataAnnotations.Schema;
using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Subscription;
using Faradars.Domain.Entities.Management.Request.Refund;
using Faradars.Domain.Entities.Payments.Transaction;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Enums;

namespace Faradars.Domain.Entities.Students.Subscription;

public class UserPlan : BaseEntity
{
    public int UserId { get; set; }
    public int PaymentTransactionId { get; set; }
    public int PlanId { get; set; }
    public DateTime StartsAt { get; set; } // equal to CreatedAt
    public DateTime FinishAt { get; set; }
    public UserPlanStatus Status { get; set; }
    public int? RefundRequestId { get; set; }

    [ForeignKey("UserId")] public User User { get; set; } = null!;
    public SubscriptionPlan Plan { get; set; } = null!;
    public Transaction Payment { get; set; } = null!;
    public RefundRequest RefundRequest { get; set; } = null!;
}