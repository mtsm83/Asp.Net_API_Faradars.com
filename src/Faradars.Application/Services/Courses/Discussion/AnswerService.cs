using Faradars.Application.DTOs.Courses.Discussion.AnswerService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Discussion;
using Faradars.Application.Interfaces.Services.Courses.Media;
using Faradars.Application.Interfaces.Services.Validation;
using Faradars.Application.Mappers.Courses.Discussion;
using Faradars.Domain.Entities.Courses.Discussion;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Discussion;

public class AnswerService(
    IRepository<QuestionAnswer> answerRepository,
    IRepository<CourseQuestion> questionRepository,
    IMediaService mediaService,
    IFluentValidatorService validator,
    IUserContextService userContextService) : IAnswerService, IScopedDependency
{
    public async Task<Result<AnswerDto>> AddAnswerAsync(AddAnswerDto dto, CancellationToken ct)
    {
        var question = await questionRepository.GetByIdAsync(ct, dto.QuestionId);
        if (question == null)
            return Result.Failure<AnswerDto>(Error.NotFound);
        var newAnswer = new QuestionAnswer();
        newAnswer.MapAddAnswerDto(dto);
        newAnswer.CreatedBy = userContextService.CurrentUser.UserId;
        if (dto.UploadedAssetFile != null)
        {
            var uploadResult = await mediaService.UploadAssetFileAsync(dto.UploadedAssetFile, ct);
            if (uploadResult.IsFailure)
                return Result.Failure<AnswerDto>(uploadResult.Error);
            var newFile = uploadResult.Value;
            newAnswer.AssetFileId = newFile.Id;
        }
        await answerRepository.AddAsync(newAnswer, ct);
        var answerDto = newAnswer.MapToAnswerDto();
        return Result.Success(answerDto);
    }

    public async Task<Result<AnswerDto>> UpdateAnswerAsync(UpdateAnswerDto dto, CancellationToken ct)
    {
        var answer = await answerRepository.GetByIdAsync(ct, dto.AnswerId);
        if (answer == null)
            return Result.Failure<AnswerDto>(Error.NotFound);

        answer.MapUpdateAnswerDto(dto);
        answer.UpdatedAt = DateTime.UtcNow;
        answer.UpdatedBy = userContextService.CurrentUser.UserId;
        // File upload & id assertion

        await answerRepository.UpdateAsync(answer, ct);
        var answerDto = answer.MapToAnswerDto();
        return Result.Success(answerDto);
    }

    public Task<Result<AnswerDto>> DeleteAnswerAsync(int answerId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<AnswerDto>>> GetAllQuestionAnswersAsync(int questionId, CancellationToken ct)
    {
        var answers = await answerRepository.TableNoTracking
            .Where(a => a.QuestionId == questionId)
            .ToListAsync(ct);
        if (answers.Count < 1)
            return Result.Failure<List<AnswerDto>>(Error.NotFound);
        var answerDtos = answers.Select(a => a.MapToAnswerDto()).ToList();
        return Result.Success(answerDtos);
    }
}