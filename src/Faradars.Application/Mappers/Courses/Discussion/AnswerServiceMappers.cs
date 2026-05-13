using Faradars.Application.DTOs.Courses.Discussion.AnswerService;
using Faradars.Domain.Entities.Courses.Discussion;

namespace Faradars.Application.Mappers.Courses.Discussion;

public static class AnswerServiceMappers
{
    public static void MapAddAnswerDto(this QuestionAnswer answer, AddAnswerDto dto)
    {
        answer.QuestionId = dto.QuestionId;
        answer.Body = dto.Body;
    }

    public static void MapUpdateAnswerDto(this QuestionAnswer answer, UpdateAnswerDto dto)
    {
        answer.Body = dto.Body;
    }

    public static AnswerDto MapToAnswerDto(this QuestionAnswer answer)
    {
        return new AnswerDto
        {
            QuestionId = answer.QuestionId,
            Body = answer.Body,
            AssetFileId = answer.AssetFileId,
            CreatedAt = answer.CreatedAt,
            CreatorId = answer.CreatedBy,
            UpdatedAt = answer.UpdatedAt,
            UpdaterId = answer.UpdatedBy,
            DeletedAt = answer.DeletedAt,
            DeleterId = answer.DeletedBy,
        };
    }
}