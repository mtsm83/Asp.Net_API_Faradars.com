namespace Faradars.Application.DTOs.Courses.Discussion.QuestionService;

public class QuestionDto
{
    public int CourseId { get; set; }
    public int LessonId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public int? AssetFileId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
}