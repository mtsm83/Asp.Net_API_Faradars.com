using Faradars.Application.DTOs.Students.Subscription.UserPlanService;
using Faradars.Domain.Entities.Students.Subscription;

namespace Faradars.Application.Mappers.Students.Subscription;

public static class UserPlanServiceMappers
{
    public static UserPlan MapAddUserPlanDto(this AddUserPlanDto dto, int currentUserId)
    {
        return new UserPlan
        {
            UserId = dto.StudentId,
            PlanId = dto.PlanId,
            PaymentTransactionId = dto.PaymentTransactionId,
            StartsAt = dto.StartsAt,
            CreatedBy = currentUserId,
            FinishAt = dto.FinishAt
        };
    }

    public static void MapUpdateUserPlanDto(this UserPlan entity, UpdateUserPlanDto dto, int currentUserId)
    {
        entity.Status = dto.Status ?? entity.Status;
        entity.RefundRequestId = dto.RefundRequestId;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = currentUserId;
    }

    public static UserPlanDto MapToUserPlanDto(this UserPlan entity)
    {
        return new UserPlanDto
        {
            UserPlanId = entity.Id,
            StudentId = entity.UserId,
            PlanId = entity.PlanId,
            PaymentTransactionId = entity.PaymentTransactionId,
            StartsAt = entity.StartsAt,
            FinishAt = entity.FinishAt,
            Status = entity.Status,
            RefundRequestId = entity.RefundRequestId
        };
    }
}