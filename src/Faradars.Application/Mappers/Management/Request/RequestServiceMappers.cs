using Faradars.Application.DTOs.Management.Request.RequestService;
using Faradars.Domain.Entities.Management.Request.Content;
using Faradars.Domain.Enums;

namespace Faradars.Application.Mappers.Management.Request;

public static class RequestServiceMappers
{
    public static UserRequest MapAddRequest(
        this AddRequestDto dto,
        int currentUserId)
    {
        return new UserRequest
        {
            UserId = currentUserId,
            SubjectId = dto.SubjectId,
            Status = RequestStatus.Pending
        };
    }

    public static void MapUpdateRequest(
        this UserRequest entity,
        UpdateRequestDto dto,
        int currentUserId)
    {
        entity.SubjectId = dto.SubjectId ?? entity.SubjectId;
        entity.UpdatedBy = currentUserId;
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public static RequestDto MapToRequestDto(
        this UserRequest entity)
    {
        return new RequestDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            SubjectId = entity.SubjectId,
            Status = entity.Status,
            CompletedAt = entity.CompletedAt,
            CreatedBy = entity.CreatedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedBy = entity.UpdatedBy,
            UpdatedAt = entity.UpdatedAt
        };
    }
}