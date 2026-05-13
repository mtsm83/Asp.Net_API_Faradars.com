using Microsoft.AspNetCore.Http;

namespace Faradars.Application.DTOs.Courses.Discussion.AnswerService;

public class UpdateAnswerDto
{
    public int AnswerId { get; set; }
    public string Body { get; set; } = null!;
    public IFormFile? UploadedAssetFile { get; set; }
}