namespace Faradars.Application.DTOs.Students.Enrollment.EnrollmentService;

public class DismissalTypeDto
{
    public int TypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int CreatorId { get; set; }
}