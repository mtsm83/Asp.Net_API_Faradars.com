using Faradars.Application.DTOs.Courses.Discussion.QuestionService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Content;
using Faradars.Application.Interfaces.Services.Courses.Discussion;
using Faradars.Application.Interfaces.Services.Courses.Media;
using Faradars.Application.Interfaces.Services.Validation;
using Faradars.Application.Mappers.Courses.Discussion;
using Faradars.Domain.Entities.Courses.Discussion;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Discussion;

public class QuestionService(
    IRepository<QuestionAnswer> answerRepository,
    IRepository<CourseQuestion> questionRepository,
    IMediaService mediaService,
    ILessonService lessonService,
    IFluentValidatorService validator,
    IUserContextService userContextService) : IQuestionService, IScopedDependency
{
    public async Task<Result<QuestionDto>> AddQuestionAsync(AddQuestionDto dto, CancellationToken ct)
    {
        var lessonResult = await lessonService.GetLessonByIdAsync(dto.LessonId, ct);
        if (lessonResult.IsFailure)
            return Result.Failure<QuestionDto>(lessonResult.Error);
        var newQuestion = dto.MapAddQuestionDto();
        newQuestion.CreatedBy = userContextService.CurrentUser.UserId;
        if (dto.File != null)
        {
            var uploadResult = await mediaService.UploadAssetFileAsync(dto.File, ct);
            if (uploadResult.IsFailure)
                return Result.Failure<QuestionDto>(uploadResult.Error);
            var newFile = uploadResult.Value;
            newQuestion.AssetFileId = newFile.Id;
        }

        await questionRepository.AddAsync(newQuestion, ct);
        var questionDto = newQuestion.MapToQuestionDto();
        return Result.Success(questionDto);
    }

    public async Task<Result<QuestionDto>> UpdateQuestionAsync(UpdateQuestionDto dto, CancellationToken ct)
    {
        var question = await questionRepository.GetByIdAsync(ct, dto.QuestionId);
        if (question == null)
            return Result.Failure<QuestionDto>(Error.NotFound);
        question.MapUpdateQuestionDto(dto);
        if (dto.File != null)
        {
            // if (question.AssetFileId != null)
            // {
            //     var resultDeletion = await mediaService.DeleteAssetFileAsync(question.AssetFileId, ct);
            //     if (resultDeletion.IsFailure)
            //         return Result.Failure<QuestionDto>(resultDeletion.Error);
            // }
            var uploadResult = await mediaService.UploadAssetFileAsync(dto.File, ct);
            if (uploadResult.IsFailure)
                return Result.Failure<QuestionDto>(uploadResult.Error);
            var newFile = uploadResult.Value;
            question.AssetFileId = newFile.Id;
        }
        question.UpdatedAt = DateTime.Now;
        question.UpdatedBy = userContextService.CurrentUser.UserId;
        await questionRepository.UpdateAsync(question, ct);
        var questionDto = question.MapToQuestionDto();
        return Result.Success(questionDto);
    }

    public async Task<Result<QuestionDto>> DeleteQuestionAsync(int questionId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<QuestionDto>> GetQuestionByIdAsync(int questionId, CancellationToken ct)
    {
        var question = await questionRepository.GetByIdAsync(ct, questionId);
        if (question == null)
            return Result.Failure<QuestionDto>(Error.NotFound);
        var questionDto = question.MapToQuestionDto();
        return Result.Success(questionDto);
    }

    public async Task<Result<List<QuestionDto>>> GetCourseQuestionsAsync(int courseId, CancellationToken ct)
    {
        var questions = await questionRepository.TableNoTracking
            .Where(q => q.CourseId == courseId).ToListAsync(ct);
        if (questions.Count == 0)
            return Result.Failure<List<QuestionDto>>(Error.NotFound);
        var questionDtos = questions.Select(q => q.MapToQuestionDto()).ToList();
        return Result.Success(questionDtos);
    }
}