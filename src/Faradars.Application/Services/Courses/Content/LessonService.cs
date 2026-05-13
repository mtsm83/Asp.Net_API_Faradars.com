using Faradars.Application.DTOs.Courses.Content.LessonService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Content;
using Faradars.Application.Interfaces.Services.Courses.Media;
using Faradars.Application.Mappers.Courses.Content;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Shared.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Content;

public class LessonService(
    IUserContextService userContextService,
    IMediaService mediaService,
    IRepository<Section> sectionRepository,
    IRepository<Lesson> lessonRepository,
    IRepository<LessonAsset> lessonAssetRepository
) : ILessonService, IScopedDependency
{
    public async Task<Result<LessonDto>> AddLessonAsync(AddLessonDto dto, CancellationToken ct)
    {
        var newLesson = new Lesson();
        newLesson.MapAddLessonDto(dto);
        newLesson.CreatedBy = userContextService.CurrentUser.UserId;
        await lessonRepository.AddAsync(newLesson, ct);
        var lessonDto = new LessonDto();
        newLesson.MapToLessonDto(lessonDto);
        return Result.Success(lessonDto);
    }

    public async Task<Result<LessonDto>> AddAssetFileToLessonAsync(AddAssetFileDto dto, CancellationToken ct)
    {
        var uploadResult = await mediaService.UploadAssetFileAsync(dto.File, ct);
        if (uploadResult.IsFailure)
            return Result.Failure<LessonDto>(uploadResult.Error);
        var newFile = uploadResult.Value;
        var lesson = await lessonRepository.GetByIdAsync(ct, dto.LessonId);
        if (lesson is null)
            return Result.Failure<LessonDto>(Error.NotFound);
        var newLessonAsset = new LessonAsset();
        newLessonAsset.AssetFileId = newFile.Id;
        newLessonAsset.LessonId = dto.LessonId;
        newLessonAsset.CreatedBy = userContextService.CurrentUser.UserId;
        await lessonAssetRepository.AddAsync(newLessonAsset, ct);
        var lessonDto = new LessonDto();
        lesson.MapToLessonDto(lessonDto);
        return Result.Success(lessonDto);
    }

    public async Task<Result<LessonDto>> DeleteAssetFileThanLessonAsync(int fileId, int lessonId, CancellationToken ct)
    {
        var lesson = await lessonRepository.GetByIdAsync(ct, lessonId);
        if (lesson is null)
            return Result.Failure<LessonDto>(Error.NotFound);
        var lessonAsset = await lessonAssetRepository.TableNoTracking
            .FirstOrDefaultAsync(x => x.AssetFileId == fileId && x.LessonId == lessonId, ct);
        if (lessonAsset is null)
            return Result.Failure<LessonDto>(Error.NotFound);
        var deletionResult = await mediaService.DeleteAssetFileAsync(fileId, ct);
        if (deletionResult.IsFailure)
            return Result.Failure<LessonDto>(deletionResult.Error);
        await lessonAssetRepository.DeleteAsync(lessonAsset, ct);
        await lessonRepository.DeleteAsync(lesson, ct);
        var lessonDto = new LessonDto();
        lesson.MapToLessonDto(lessonDto);
        return Result.Success(lessonDto);
    }

    public async Task<Result<LessonDto>> UpdateLessonAsync(UpdateLessonDto dto, CancellationToken ct)
    {
        var lesson = await lessonRepository.TableNoTracking
            .FirstOrDefaultAsync(l => l.Id == dto.Id, ct);
        if (lesson == null)
            return Result.Failure<LessonDto>(Error.NotFound);
        lesson.MapLessonUpdateDto(dto);
        lesson.UpdatedAt = DateTime.Now;
        lesson.UpdatedBy = userContextService.CurrentUser.UserId;
        await lessonRepository.UpdateAsync(lesson, ct);
        var lessonDto = new LessonDto();
        lesson.MapToLessonDto(lessonDto);
        return Result.Success(lessonDto);
    }

    public async Task<Result<LessonDto>> DeleteLessonAsync(int lessonId, CancellationToken ct)
    {
        // todo: what will happen to assets and derivations after deletion?

        throw new NotImplementedException();
        // var lesson = await lessonRepository.TableNoTracking
        //     .FirstOrDefaultAsync(l => l.Id == lessonId, ct);
        //
        // if (lesson == null)
        //     return Result.Failure<Unit>(Error.NotFound);
        //
        // await lessonRepository.DeleteAsync(lesson, ct);
        // return Result.Success(Unit.Value);
    }

    public async Task<Result<List<LessonDto>>> GetAllSectionLessonsAsync(int sectionId, CancellationToken ct)
    {
        var section = await sectionRepository.TableNoTracking
            .Include(cs => cs.Lessons)
            .ThenInclude(l => l.LessonAssets)
            .FirstOrDefaultAsync(s => s.Id == sectionId, ct);

        if (section == null)
            return Result.Failure<List<LessonDto>>(Error.NotFound);

        var lessonDtos = section.Lessons.Select(l =>
        {
            var lDto = new LessonDto();
            l.MapToLessonDto(lDto);
            return lDto;
        }).ToList();

        return Result.Success(lessonDtos);
    }

    public async Task<Result<LessonDto>> GetLessonByIdAsync(int lessonId, CancellationToken ct)
    {
        var lesson = await lessonRepository.TableNoTracking
            .FirstOrDefaultAsync(l => l.Id == lessonId, ct);
        if (lesson == null)
            return Result.Failure<LessonDto>(Error.NotFound);
        var lessonDto = new LessonDto();
        lesson.MapToLessonDto(lessonDto);
        return Result.Success(lessonDto);
    }

    public Task<Result<bool>> MarkLessonCompleteAsync(MarkCompleteDto dto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}