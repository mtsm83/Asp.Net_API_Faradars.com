namespace Faradars.Application.DTOs.Courses.Discussion.ReviewService;

public class AddReviewDto
{
    public int UserId { get; init; }
    public int CourseId { get; init; }
    public int Rating { get; init; } // 1–5
    public string Comment { get; init; } = null!;
}