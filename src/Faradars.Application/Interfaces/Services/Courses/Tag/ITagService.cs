using Faradars.Application.DTOs.Courses.Tag.TagService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Tag;

public interface ITagService
{
    Task<Result<TagDto>> AddTagAsync(AddTagDto dto, CancellationToken ct);
    Task<Result<TagDto>> UpdateTagAsync(UpdateTagDto dto, CancellationToken ct);
    Task<Result<TagDto>> DeleteTagAsync(int tagId, CancellationToken ct);
    Task<Result<Unit>> AssignCourseToTagAsync(int courseId, int tagId, CancellationToken ct);
    Task<Result<Unit>> RemoveCourseThanTagAsync(int courseId, int tagId, CancellationToken ct);
    Task<Result<TagDto>> GetTagByIdAsync(int id, CancellationToken ct);
    Task<Result<List<TagDto>>> GetAllTagsAsync(CancellationToken ct);
    Task<Result<List<TagDto>>> GetTagsByCourseIdAsync(int courseId, CancellationToken ct);
}