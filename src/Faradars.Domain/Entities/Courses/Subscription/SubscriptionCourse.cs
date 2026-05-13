using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;

namespace Faradars.Domain.Entities.Courses.Subscription;

public class SubscriptionCourse : BaseEntity
{
    public int PlanId { get; set; }
    public int CourseId { get; set; }
    
    public SubscriptionPlan Plan { get; set; }
    public Course Course { get; set; }
}