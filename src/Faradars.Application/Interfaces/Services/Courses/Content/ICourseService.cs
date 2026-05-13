using Faradars.Application.DTOs.Courses.Content.CourseService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Content;

public interface ICourseService
{
    Task<Result<PreviewCourseDto>> AddCourseAsync(AddCourseDto dto, CancellationToken ct);
    Task<Result<PreviewCourseDto>> UpdateCourseAsync(UpdateCourseDto dto, CancellationToken ct);
    Task<Result<PreviewCourseDto>> UpdateCourseAverageRatingAsync(int courseId, CancellationToken ct);
    Task<Result<PreviewCourseDto>> DeleteCourseAsync(int courseId, CancellationToken ct);
    Task<Result<List<PreviewCourseDto>>> GetAllCoursesAsync(CancellationToken ct);
    Task<Result<List<PreviewCourseDto>>> GetAllSearchedCoursesAsync(string searchText, CancellationToken ct);
    Task<Result<PreviewCourseDto>> GetCourseByIdAsync(int courseId, CancellationToken ct);
    Task<Result<List<PreviewCourseDto>>> GetCoursesByCategoryAsync(int categoryId, CancellationToken ct);
    Task<Result<List<PreviewCourseDto>>> GetCoursesByTagAsync(int tagId, CancellationToken ct);
    Task<Result<List<PreviewCourseDto>>> GetAllOfferCoursesId(int offerId, CancellationToken ct);
    Task<Result<List<PreviewCourseDto>>> GetAllBundleCoursesAsync(int bundle, CancellationToken ct);
    Task<Result<List<PreviewCourseDto>>> GetAllSubscriptionCoursesAsync(int subscriptionId, CancellationToken ct);
    Task<Result<List<PreviewCourseDto>>> GetAllTeacherCoursesAsync(int teacherId, CancellationToken ct);
}