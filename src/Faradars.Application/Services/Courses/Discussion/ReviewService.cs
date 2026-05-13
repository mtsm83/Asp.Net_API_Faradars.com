using Faradars.Application.DTOs.Courses.Discussion.ReviewService;
using Faradars.Application.Interfaces.General;
using Faradars.Application.Interfaces.Services.Courses.Content;
using Faradars.Application.Interfaces.Services.Courses.Discussion;
using Faradars.Application.Mappers.Courses.Discussion;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Courses.Discussion;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Shared.Result;
using Microsoft.EntityFrameworkCore;

namespace Faradars.Application.Services.Courses.Discussion;

public class ReviewService(
    IUserContextService userContextService,
    IRepository<Course> courseRepository,
    ICourseService courseService,
    IRepository<User> UserRepository,
    IRepository<CourseReview> reviewRepository
) : IReviewService, IScopedDependency
{
    public async Task<Result<ReviewDto>> AddReviewAsync(AddReviewDto dto, CancellationToken ct)
    {
        var course = await courseRepository.GetByIdAsync(ct, dto.CourseId);
        if (course == null)
            return Result.Failure<ReviewDto>(Error.CourseNotFound);
        var existingReview = await reviewRepository.Table
            .FirstOrDefaultAsync(r => r.CourseId ==
                dto.CourseId && r.UserId == userContextService.CurrentUser.UserId, ct);
        if (existingReview != null)
            return Result.Failure<ReviewDto>(Error.AlreadyExists);
        var newReview = dto.MapAddReviewDto();
        newReview.CreatedBy = userContextService.CurrentUser.UserId;
        var courseUpdateResult = await courseService.UpdateCourseAverageRatingAsync(dto.CourseId, ct);
        if (courseUpdateResult.IsFailure)
            return Result.Failure<ReviewDto>(Error.CourseNotFound);
        await reviewRepository.AddAsync(newReview, ct);
        var reviewDto = newReview.MapToReviewDto();
        return Result.Success(reviewDto);
    }

    public async Task<Result<ReviewDto>> DeleteReviewAsync(int reviewId, CancellationToken ct)
    {
        var review = await reviewRepository.GetByIdAsync(ct, reviewId);
        if (review == null)
            return Result.Failure<ReviewDto>(Error.NotFound);
        if (review.UserId != userContextService.CurrentUser.UserId &&
            !userContextService.CurrentUser.IsAdmin)
            return Result.Failure<ReviewDto>(Error.Unauthorized);
        await reviewRepository.DeleteAsync(review, ct);
        var courseUpdateResult = await courseService.UpdateCourseAverageRatingAsync(review.CourseId, ct);
        if (courseUpdateResult.IsFailure)
            return Result.Failure<ReviewDto>(Error.CourseNotFound);
        var reviewDto = review.MapToReviewDto();
        return Result.Success(reviewDto);
    }

    public async Task<Result<ReviewDto>> GetReviewByIdAsync(int reviewId, CancellationToken ct)
    {
        var review = await reviewRepository.GetByIdAsync(ct, reviewId);
        if (review == null)
            return Result.Failure<ReviewDto>(Error.NotFound);
        if (review.UserId != userContextService.CurrentUser.UserId &&
            !userContextService.CurrentUser.IsAdmin)
            return Result.Failure<ReviewDto>(Error.Unauthorized);
        var reviewDto = review.MapToReviewDto();
        return Result.Success(reviewDto);
    }

    public async Task<Result<List<ReviewDto>>> GetCourseReviewsAsync(int courseId, CancellationToken ct)
    {
        var reviews = await reviewRepository.TableNoTracking.Where(r => r.CourseId == courseId)
            .ToListAsync(ct);
        if (reviews.Count < 1)
            return Result.Failure<List<ReviewDto>>(Error.NotFound);

        var reviewDtos = reviews.Select(r => r.MapToReviewDto()).ToList();
        return Result.Success(reviewDtos);
    }

    public async Task<Result<List<ReviewDto>>> GetAllUserReviewsAsync(int userId, CancellationToken ct)
    {
        var reviews = await reviewRepository.TableNoTracking.Where(r => r.UserId == userId)
            .ToListAsync(ct);
        if (reviews.Count < 1)
            return Result.Failure<List<ReviewDto>>(Error.NotFound);

        var reviewDtos = reviews.Select(r => r.MapToReviewDto()).ToList();
        return Result.Success(reviewDtos);
    }
}