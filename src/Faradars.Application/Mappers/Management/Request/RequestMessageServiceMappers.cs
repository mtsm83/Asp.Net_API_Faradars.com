using Faradars.Application.DTOs.Management.Request.RequestMessageService;
using Faradars.Domain.Entities.Management.Request.Content;

namespace Faradars.Application.Mappers.Management.Request;

public static class RequestMessageServiceMappers
{
    public static RequestMessage MapAddRequestMessage(
        this AddRequestMessage dto,
        int currentUserId)
    {
        return new RequestMessage
        {
            RequestId = dto.RequestId,
            Body = dto.Body,
            Order = dto.Order,
            CreatedBy = currentUserId
        };
    }

    public static void MapUpdateRequestMessage(
        this RequestMessage entity,
        UpdateMessageRequest dto,
        int currentUserId)
    {
        entity.Body = dto.Body;
        entity.Order = dto.Order;
        entity.UpdatedBy = currentUserId;
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public static RequestMessageDto MapToRequestMessageDto(
        this RequestMessage entity)
    {
        return new RequestMessageDto
        {
            MessageId = entity.Id,
            RequestId = entity.RequestId,
            Body = entity.Body,
            CreatedBy = entity.CreatedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedBy = entity.UpdatedBy,
            UpdatedAt = entity.UpdatedAt
        };
    }
}