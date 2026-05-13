using Faradars.Application.DTOs.Courses.Discussion.ReviewService;
using Faradars.Shared.Result;

namespace Faradars.Application.Interfaces.Services.Courses.Discussion;

public interface IReviewService
{
    Task<Result<ReviewDto>> AddReviewAsync(AddReviewDto dto, CancellationToken ct);
    Task<Result<ReviewDto>> DeleteReviewAsync(int reviewId, CancellationToken ct);
    Task<Result<ReviewDto>> GetReviewByIdAsync(int reviewId, CancellationToken ct);
    Task<Result<List<ReviewDto>>> GetCourseReviewsAsync(int courseId, CancellationToken ct);
    Task<Result<List<ReviewDto>>> GetAllUserReviewsAsync(int userId, CancellationToken ct);
}