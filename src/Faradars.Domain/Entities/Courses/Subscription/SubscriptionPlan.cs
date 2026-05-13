using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Payments.Cart;
using Faradars.Domain.Entities.Students.Subscription;

namespace Faradars.Domain.Entities.Courses.Subscription;

public class SubscriptionPlan : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public bool AllowDownload { get; set; } = false;
    public TimeSpan Duration { get; set; }
    public DateTime? EventAt { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public ICollection<UserPlan> UserPlans { get; set; } = new List<UserPlan>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<SubscriptionCourse> SubscriptionCourses { get; set; } = new List<SubscriptionCourse>();
    
}