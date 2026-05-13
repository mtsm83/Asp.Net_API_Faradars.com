using Microsoft.AspNetCore.Http;

namespace Faradars.Application.DTOs.Courses.Content.LessonService;

public class AddAssetFileDto
{
    public IFormFile File { get; set; }
    public int LessonId { get; set; }
}