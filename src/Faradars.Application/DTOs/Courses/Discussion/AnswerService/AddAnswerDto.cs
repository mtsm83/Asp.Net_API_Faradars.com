using Microsoft.AspNetCore.Http;

namespace Faradars.Application.DTOs.Courses.Discussion.AnswerService;

public class AddAnswerDto
{
    public int QuestionId { get; set; }
    public string Body { get; set; } = null!;
    public IFormFile? UploadedAssetFile { get; set; }
    
}