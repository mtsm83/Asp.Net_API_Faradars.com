using Microsoft.AspNetCore.Http;

namespace Faradars.Application.DTOs.Courses.Discussion.QuestionService;

public class AddQuestionDto
{
    public int CourseId { get; set; }
    public int LessonId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public IFormFile? File { get; set; } = null!;
}