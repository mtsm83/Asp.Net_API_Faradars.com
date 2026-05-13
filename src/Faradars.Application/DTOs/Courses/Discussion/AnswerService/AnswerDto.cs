namespace Faradars.Application.DTOs.Courses.Discussion.AnswerService;

public class AnswerDto
{
    public int QuestionId { get; set; }
    public string Body { get; set; } = null!;
    public int? AssetFileId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? UpdaterId { get; set; }    
    public DateTime? DeletedAt { get; set; }
    public int? DeleterId { get; set; }
}