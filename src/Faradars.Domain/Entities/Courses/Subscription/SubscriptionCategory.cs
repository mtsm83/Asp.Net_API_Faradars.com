using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Courses.Subscription;

public class SubscriptionCategory: BaseEntity
{
    public int PlanId { get; set; }
    public int CategoryId { get; set; }
    
    public SubscriptionPlan Plan { get; set; }
    public Category.Category Category { get; set; }
}