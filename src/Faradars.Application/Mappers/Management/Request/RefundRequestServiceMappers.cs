using Faradars.Application.DTOs.Management.Request.RefundRequestService;
using Faradars.Domain.Entities.Management.Request.Refund;

namespace Faradars.Application.Mappers.Management.Request;

public static class RefundRequestServiceMappers
{
    public static RefundRequest MapAddRefundRequest(
        this AddRefundRequestDto dto,
        int currentUserId)
    {
        return new RefundRequest
        {
            UserRequestId = dto.RequestId,
            OrderItemId = dto.OrderItemId,
            RefundAmount = dto.RefundAmount,
            CreatedBy = currentUserId,
        };
    }

    public static void MapUpdateRefundRequest(
        this RefundRequest entity,
        UpdateRefundRequestDto dto,
        int currentUserId)
    {
        entity.OrderItemId = dto.OrderItemId;
        entity.RefundAmount = dto.RefundAmount;
        entity.UpdatedBy = currentUserId;
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public static RefundRequestDto MapToRefundRequestDto(
        this RefundRequest entity)
    {
        return new RefundRequestDto
        {
            RefundRequestId = entity.Id,
            UserRequestId = entity.UserRequestId,
            OrderItemId = entity.OrderItemId,
            RefundAmount = entity.RefundAmount,
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy,
            UpdatedAt = entity.UpdatedAt,
        };
    }

}