using Faradars.Application.DTOs.Courses.Content.SectionService;
using Faradars.Domain.Entities.Courses.Content;

namespace Faradars.Application.Mappers.Courses.Content;

public static class SectionServiceMappers
{
    public static void MapAddSectionDto(this Section section, AddSectionDto dto)
    {
        section.CourseId = dto.CourseId;
        section.Name = dto.Name;
        section.Order = dto.Order;
        section.Description = dto.Description;
    }

    public static void MapUpdateSectionDto(this Section section, UpdateSectionDto dto)
    {
        section.Name = dto.Name;
        section.Order = dto.Order;
        section.Description = dto.Description;
    }

    public static SectionDto MapToSectionDto(this Section section)
    {
        return new SectionDto
        {
            SectionId = section.Id,
            CourseId = section.CourseId,
            Name = section.Name,
            Order = section.Order,
            Description = section.Description,
            CreatedAt = section.CreatedAt,
            CreatorId = section.CreatedBy,
            UpdatedAt = section.UpdatedAt,
            UpdaterId = section.UpdatedBy,
            DeletedAt = section.DeletedAt,
            DeleterId = section.DeletedBy
        };
    }
}