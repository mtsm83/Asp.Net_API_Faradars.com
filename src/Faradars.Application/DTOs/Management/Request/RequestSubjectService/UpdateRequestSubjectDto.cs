namespace Faradars.Application.DTOs.Management.Request.RequestSubjectService;

public class UpdateRequestSubjectDto
{
    public int SubjectId { get; set; }
    public string Title { get; set; } = null!; 
    public string? Description { get; set; }
}