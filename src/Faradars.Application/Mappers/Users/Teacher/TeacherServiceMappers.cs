using Faradars.Application.DTOs.Users.Teacher.TeacherService;
using Faradars.Domain.Entities.Users.Teacher;

namespace Faradars.Application.Mappers.Users.Teacher;

public static class TeacherServiceMappers
{
    public static Domain.Entities.Users.Teacher.Teacher MapAddTeacherDto(this AddTeacherDto dto)
    {
        return new Domain.Entities.Users.Teacher.Teacher
        {
            UserId = dto.UserId,
            Bio = dto.Bio,
            LinkedinUrl = dto.LinkedinUrl
        };
    }

    public static void MapUpdateTeacherDto(this Domain.Entities.Users.Teacher.Teacher teacher, UpdateTeacherDto dto)
    {
        teacher.Bio = dto.Bio;
        teacher.LinkedinUrl = dto.LinkedinUrl;
    }

    public static void MapAddTeacherDismissalDto(this TeacherDismissal teacherDismissal, DismissTeacherDto dto)
    {
        teacherDismissal.TeacherId = dto.TeacherId;
        teacherDismissal.DismissalReason = dto.DismissalReason;
        teacherDismissal.DismissalTypeId = dto.DismissalTypeId;
    }

    public static TeacherDto MapToTeacherDto(this Domain.Entities.Users.Teacher.Teacher teacher)
    {
        return new TeacherDto
        {
            UserId = teacher.UserId,
            TeacherId = teacher.Id,
            Bio = teacher.Bio,
            LinkedinUrl = teacher.LinkedinUrl,
        };
    }

    public static TeacherDismissalDto MapToTeacherDismissalDto(this TeacherDismissal teacherDismissal)
    {
        return new TeacherDismissalDto
        {
            DismissalId = teacherDismissal.Id,
            DismissalReason = teacherDismissal.DismissalReason,
            DismissalTypeId = teacherDismissal.DismissalTypeId,
            TeacherId = teacherDismissal.TeacherId,
            CreatorId = teacherDismissal.CreatedBy,
            UpdaterId = teacherDismissal.UpdatedBy,
            UpdatedAt = teacherDismissal.UpdatedAt,
            CreatedAt = teacherDismissal.CreatedAt,
        };
    }
}