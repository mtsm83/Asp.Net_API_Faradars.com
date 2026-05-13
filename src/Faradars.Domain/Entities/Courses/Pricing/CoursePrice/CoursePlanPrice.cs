using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;

namespace Faradars.Domain.Entities.Courses.Pricing.CoursePrice;

public class CoursePlanPrice : BaseEntity
{
    public int CourseId { get; set; }
    public int PlanId { get; set; } // if the plan is premium the price is zero
    public decimal Price { get; set; } // 0 Rial for premium plan

    public Course Course { get; set; } = null!;
}