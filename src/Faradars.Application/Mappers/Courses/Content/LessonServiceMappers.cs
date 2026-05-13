using Faradars.Application.DTOs.Courses.Content.LessonService;
using Faradars.Domain.Entities.Courses.Content;

namespace Faradars.Application.Mappers.Courses.Content;

public static class LessonServiceMappers
{
    public static void MapToLessonDto(this Lesson lesson, LessonDto dto)
    {
        dto.CourseId = lesson.CourseId;
        dto.SectionId = lesson.SectionId;
        dto.LessonId = lesson.Id;
        dto.Name = lesson.Name;
        dto.Duration = lesson.Duration;
        dto.CreatedAt = lesson.CreatedAt;
        dto.UpdatedAt = lesson.UpdatedAt;
        dto.UpdaterId = lesson.UpdatedBy;
        dto.DeletedAt = lesson.DeletedAt;
        dto.DeleterId = lesson.DeletedBy;
    }

    public static void MapAddLessonDto(this Lesson lesson, AddLessonDto dto)
    {
        lesson.CourseId = dto.CourseId;
        lesson.SectionId = dto.SectionId;
        lesson.Name = dto.Name;
        lesson.Order = dto.Order;
        lesson.Description = dto.Description;
        lesson.IsFree = dto.IsFree;
    }
    public static void MapLessonUpdateDto(this Lesson lesson, UpdateLessonDto dto)
    {
        lesson.CourseId = dto.CourseId;
        lesson.SectionId = dto.SectionId;
        lesson.Name = dto.Name;
        lesson.Order = dto.Order;
        lesson.Description = dto.Description;
        lesson.IsFree = dto.IsFree;
    }
}