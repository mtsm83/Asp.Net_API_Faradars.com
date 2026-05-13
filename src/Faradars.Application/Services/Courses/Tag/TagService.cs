using Faradars.Application.DTOs.Courses.Tag.TagService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Tag;
using Faradars.Application.Mappers.Courses.Tag;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Courses.Tag;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Tag;

public class TagService(
    IRepository<Domain.Entities.Courses.Tag.Tag> tagRepository,
    IRepository<Course> courseRepository,
    IRepository<CourseTag> courseTagRepository,
    IUserContextService userContextService) : ITagService, IScopedDependency
{
    public async Task<Result<TagDto>> AddTagAsync(AddTagDto dto, CancellationToken ct)
    {
        var existingTag = await tagRepository.TableNoTracking
            .FirstOrDefaultAsync(t => t.Title == dto.Title, ct);
        if (existingTag != null)
            return Result.Failure<TagDto>(Error.AlreadyExists);
        var newTag = dto.MapAddDtoToTag();
        newTag.CreatedBy = userContextService.CurrentUser.UserId;
        await tagRepository.AddAsync(newTag, ct);
        var tagDto = newTag.MapToTagDto();
        return Result.Success(tagDto);
    }

    public async Task<Result<TagDto>> UpdateTagAsync(UpdateTagDto dto, CancellationToken ct)
    {
        var tag = await tagRepository.TableNoTracking
            .FirstOrDefaultAsync(t => t.Id == dto.TagId, ct);

        if (tag == null)
            return Result.Failure<TagDto>(Error.NotFound);

        tag.MapUpdateDtoToTag(dto);
        tag.UpdatedBy = userContextService.CurrentUser.UserId;
        tag.UpdatedAt = DateTime.UtcNow;

        await tagRepository.UpdateAsync(tag, ct);
        var tagDto = tag.MapToTagDto();
        return Result.Success(tagDto);
    }

    public async Task<Result<TagDto>> DeleteTagAsync(int tagId, CancellationToken ct)
    {
        var tag = await tagRepository.TableNoTracking
            .Include(t => t.CourseTags)
            .FirstOrDefaultAsync(t => t.Id == tagId, ct);

        if (tag == null)
            return Result.Failure<TagDto>(Error.NotFound);

        await tagRepository.DeleteAsync(tag, ct);
        var tagDto = tag.MapToTagDto();
        return Result.Success(tagDto);
    }

    public async Task<Result<Unit>> AssignCourseToTagAsync(int courseId, int tagId, CancellationToken ct)
    {
        var course = await courseRepository.GetByIdAsync(ct, courseId);
        if (course == null)
            return Result.Failure<Unit>(Error.NotFound);

        var tag = await tagRepository.GetByIdAsync(ct, tagId);
        if (tag == null)
            return Result.Failure<Unit>(Error.NotFound);

        var existingAssignment = await courseTagRepository.TableNoTracking
            .FirstOrDefaultAsync(ct => ct.CourseId == courseId && ct.TagId == tagId, ct);

        if (existingAssignment != null)
            return Result.Failure<Unit>(Error.AlreadyExists);

        var courseTag = new CourseTag
        {
            CourseId = courseId,
            TagId = tagId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userContextService.CurrentUser.UserId
        };

        await courseTagRepository.AddAsync(courseTag, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<Unit>> RemoveCourseThanTagAsync(int courseId, int tagId, CancellationToken ct)
    {
        var courseTag = await courseTagRepository.TableNoTracking
            .FirstOrDefaultAsync(ct => ct.CourseId == courseId && ct.TagId == tagId, ct);

        if (courseTag == null)
            return Result.Failure<Unit>(Error.NotFound);
        await courseTagRepository.DeleteAsync(courseTag, ct);
        return Result.Success(Unit.Value);
    }

    public async Task<Result<TagDto>> GetTagByIdAsync(int id, CancellationToken ct)
    {
        var tag = await tagRepository.TableNoTracking
            .FirstOrDefaultAsync(t => t.Id == id, ct);
        if (tag == null)
            return Result.Failure<TagDto>(Error.NotFound);
        var tagDto = tag.MapToTagDto();
        return Result.Success(tagDto);
    }

    public async Task<Result<List<TagDto>>> GetAllTagsAsync(CancellationToken ct)
    {
        var tags = await tagRepository.TableNoTracking
            .ToListAsync(ct);
        if (!tags.Any())
            return Result.Failure<List<TagDto>>(Error.NotFound);
        var tagDtos = tags.Select(t => t.MapToTagDto()).ToList();
        return Result.Success(tagDtos);
    }

    public async Task<Result<List<TagDto>>> GetTagsByCourseIdAsync(int courseId, CancellationToken ct)
    {
        var courseTags = await courseTagRepository.TableNoTracking
            .Include(ct => ct.Tag)
            .Where(ct => ct.CourseId == courseId)
            .Select(ct => ct.Tag)
            .ToListAsync(ct);

        if (!courseTags.Any())
            return Result.Failure<List<TagDto>>(Error.NotFound);
        var tagDtos = courseTags.Select(t => t.MapToTagDto()).ToList();
        return Result.Success(tagDtos);
    }
}