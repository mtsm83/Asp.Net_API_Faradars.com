using Faradars.Application.DTOs.Courses.Subsctiption.SubscriptionService;
using Faradars.Domain.Entities.Courses.Subscription;

namespace Faradars.Application.Mappers.Courses.Subsctiption;

public static class SubscriptionServiceMappers
{
    public static SubscriptionPlan MapAddSubscription(this AddSubscriptionDto dto)
    {
        return new SubscriptionPlan
        {
            Title =  dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            AllowDownload = dto.AllowDownload,
            Duration = dto.Duration,
            EventAt = dto.EventAt,
            ExpiresAt = dto.ExpiresAt,
        };
    }
    public static SubscriptionDto MapToPlanDto(this SubscriptionPlan plan)
    {
        return new SubscriptionDto
        {
            PlanId =  plan.Id,
            Title =  plan.Title,
            Description = plan.Description,
            Price = plan.Price,
            AllowDownload = plan.AllowDownload,
            Duration = plan.Duration,
            EventAt = plan.EventAt,
            ExpiresAt = plan.ExpiresAt,
            CreatedAt = plan.CreatedAt,
            CreatorId = plan.CreatedBy,
            UpdatedAt = plan.UpdatedAt,
            UpdaterId = plan.UpdatedBy,
            DeletedAt = plan.DeletedAt,
            DeleterId = plan.DeletedBy
        };
    }
    public static void MapUpdateSubscriptionDto(this SubscriptionPlan plan, UpdateSubscriptionDto dto)
    {
        plan.Title = dto.Title;
        plan.Description = dto.Description;
        plan.Price = dto.Price;
        plan.AllowDownload = dto.AllowDownload;
        plan.Duration = dto.Duration;
        plan.EventAt = dto.EventAt;
        plan.ExpiresAt = dto.ExpiresAt;
    }
    
}