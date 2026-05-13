using Microsoft.AspNetCore.Http;

namespace Faradars.Application.DTOs.Courses.Discussion.QuestionService;

public class UpdateQuestionDto
{
    public int QuestionId { get; set; }
    public string? Title { get; set; } = null!;
    public string? Body { get; set; } = null!;
    public IFormFile? File { get; set; } = null!;
}