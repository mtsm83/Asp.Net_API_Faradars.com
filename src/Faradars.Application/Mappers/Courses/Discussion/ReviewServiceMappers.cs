using Faradars.Application.DTOs.Courses.Discussion.ReviewService;
using Faradars.Domain.Entities.Courses.Discussion;

namespace Faradars.Application.Mappers.Courses.Discussion;

public static class ReviewServiceMappers
{
    public static CourseReview MapAddReviewDto(this AddReviewDto dto)
    {
        return new CourseReview
        {
            UserId = dto.UserId,
            CourseId = dto.CourseId,
            Rating = dto.Rating,
            Body = dto.Comment
        };
    }

    public static ReviewDto MapToReviewDto(this CourseReview courseReview)
    {
        return new ReviewDto
        {
            ReviewId = courseReview.Id,
            UserId = courseReview.UserId,
            CourseId = courseReview.CourseId,
            Rating = courseReview.Rating,
            Body = courseReview.Body,
            CreatedAt = courseReview.CreatedAt,
            CreatorId = courseReview.CreatedBy,
            UpdatedAt = courseReview.UpdatedAt,
            UpdaterId = courseReview.UpdatedBy,
            DeletedAt = courseReview.DeletedAt,
            DeleterId = courseReview.DeletedBy
        };
    }
}