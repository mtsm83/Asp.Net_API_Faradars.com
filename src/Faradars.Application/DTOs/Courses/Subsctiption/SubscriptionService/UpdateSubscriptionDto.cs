namespace Faradars.Application.DTOs.Courses.Subsctiption.SubscriptionService;

public class UpdateSubscriptionDto
{
    public int PlanId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public bool AllowDownload { get; set; }
    public bool IsActive { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime? EventAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}