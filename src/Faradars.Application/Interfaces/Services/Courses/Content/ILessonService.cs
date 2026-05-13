using Faradars.Application.DTOs.Courses.Content.LessonService;
using Faradars.Shared.Result;
using Microsoft.AspNetCore.Http;

namespace Faradars.Application.Interfaces.Services.Courses.Content;

public interface ILessonService
{
    Task<Result<LessonDto>> AddLessonAsync(AddLessonDto dto, CancellationToken ct);
    Task<Result<LessonDto>> AddAssetFileToLessonAsync(AddAssetFileDto dto, CancellationToken ct);
    Task<Result<LessonDto>> DeleteAssetFileThanLessonAsync(int fileId, int lessonId, CancellationToken ct);
    Task<Result<LessonDto>> UpdateLessonAsync(UpdateLessonDto dto, CancellationToken ct);
    Task<Result<LessonDto>> DeleteLessonAsync(int lessonId, CancellationToken ct);
    Task<Result<List<LessonDto>>> GetAllSectionLessonsAsync(int sectionId, CancellationToken ct);
    Task<Result<LessonDto>> GetLessonByIdAsync(int lessonId, CancellationToken ct);
    Task<Result<bool>> MarkLessonCompleteAsync(MarkCompleteDto dto, CancellationToken ct);
}