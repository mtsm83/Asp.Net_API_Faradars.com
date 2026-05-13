namespace Faradars.Application.DTOs.Students.Subscription.UserPlanService;

public class AddUserPlanDto
{
    public int StudentId { get; set; }
    public int PaymentTransactionId { get; set; }
    public int PlanId { get; set; }
    public DateTime StartsAt { get; set; } // equal to CreatedAt
    public DateTime FinishAt { get; set; } // equal to CreatedAt
}