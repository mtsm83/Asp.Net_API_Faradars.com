using Faradars.Application.DTOs.Courses.Tag.TagService;

namespace Faradars.Application.Mappers.Courses.Tag;

public static class TagServiceMappers
{
    public static Domain.Entities.Courses.Tag.Tag MapAddDtoToTag(this AddTagDto dto)
    {
        return new Domain.Entities.Courses.Tag.Tag
        {
            Title = dto.Title,
            Description = dto.Description
        };
    }

    public static TagDto MapToTagDto(this Domain.Entities.Courses.Tag.Tag entity)
    {
        return new TagDto
        {
            TagId = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            CreatorId = entity.CreatedBy,
            UpdatedAt = entity.UpdatedAt,
            UpdaterId = entity.UpdatedBy,
            DeletedAt = entity.DeletedAt
        };
    }

    public static void MapUpdateDtoToTag(this Domain.Entities.Courses.Tag.Tag entity, UpdateTagDto dto)
    {
        entity.Title = dto.Name;
        entity.Description = dto.Description;
    }
}