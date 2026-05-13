using Faradars.Domain.Enums;

namespace Faradars.Application.DTOs.Students.Subscription.UserPlanService;

public class UserPlanDto
{
    public int UserPlanId { get; set; }
    public int StudentId { get; set; }
    public int PaymentTransactionId { get; set; }
    public int PlanId { get; set; }
    public DateTime StartsAt { get; set; } // equal to CreatedAt
    public DateTime FinishAt { get; set; }
    public UserPlanStatus Status { get; set; }
    public int? RefundRequestId { get; set; }
}