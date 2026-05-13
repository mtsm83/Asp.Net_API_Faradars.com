namespace Faradars.Application.DTOs.Courses.Subsctiption.SubscriptionService;

public class SubscriptionDto
{
    public int PlanId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public bool AllowDownload { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime? EventAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }

}