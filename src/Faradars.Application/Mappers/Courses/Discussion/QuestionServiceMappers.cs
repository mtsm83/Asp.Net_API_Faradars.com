using Faradars.Application.DTOs.Courses.Discussion.QuestionService;
using Faradars.Domain.Entities.Courses.Discussion;

namespace Faradars.Application.Mappers.Courses.Discussion;

public static class QuestionServiceMappers
{
    public static CourseQuestion MapAddQuestionDto(this AddQuestionDto questionDto)
    {
        return new CourseQuestion
        {
            CourseId = questionDto.CourseId,
            LessonId = questionDto.LessonId,
            UserId = questionDto.UserId,
            Title = questionDto.Title,
            Body = questionDto.Body,
        };
    }

    public static QuestionDto MapToQuestionDto(this CourseQuestion question)
    {
        return new QuestionDto
        {
            CourseId = question.CourseId,
            LessonId = question.LessonId,
            UserId = question.UserId,
            Title = question.Title,
            Body = question.Body,
            AssetFileId = question.AssetFileId,
            CreatedAt = question.CreatedAt,
            CreatorId = question.CreatedBy,
            UpdatedAt = question.UpdatedAt,
            UpdaterId = question.UpdatedBy,
            DeletedAt = question.DeletedAt,
            DeleterId = question.DeletedBy,
        };
    }

    public static void MapUpdateQuestionDto(this CourseQuestion question, UpdateQuestionDto questionDto)
    {
        question.Title = questionDto.Title ?? question.Title;
        question.Body = questionDto.Body ?? question.Body;
    }
}