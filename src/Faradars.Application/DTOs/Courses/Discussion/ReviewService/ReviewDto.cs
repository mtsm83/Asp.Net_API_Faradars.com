namespace Faradars.Application.DTOs.Courses.Discussion.ReviewService;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public int Rating { get; set; }
    public string Body { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
    
}