namespace Faradars.Domain.Entities.Courses.Subscription;

public class SubscriptionTag
{
    public int PlanId { get; set; }
    public int TagId { get; set; }
    
    public SubscriptionPlan Plan { get; set; }
    public Tag.Tag Tag { get; set; }
}