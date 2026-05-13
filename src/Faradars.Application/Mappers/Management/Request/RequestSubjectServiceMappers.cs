using Faradars.Application.DTOs.Management.Request.RequestSubjectService;
using Faradars.Domain.Entities.Management.Request.Content;

namespace Faradars.Application.Mappers.Management.Request;

public static class RequestSubjectServiceMappers
{
    public static RequestSubject MapAddRequestSubject(
        this AddRequestSubjectDto dto,
        int currentUserId)
    {
        return new RequestSubject
        {
            Title = dto.Title,
            Description = dto.Description,
            CreatedBy = currentUserId,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void MapUpdateRequestSubject(
        this RequestSubject entity,
        UpdateRequestSubjectDto dto,
        int currentUserId)
    {
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.UpdatedBy = currentUserId;
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public static SubjectDto MapToSubjectDto(
        this RequestSubject entity)
    {
        return new SubjectDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            CreatedBy = entity.CreatedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedBy = entity.UpdatedBy,
            UpdatedAt = entity.UpdatedAt
        };
    }
}