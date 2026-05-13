using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Courses.Content;

public class CourseDeActivation : BaseEntity
{
    public int CourseId { get; set; }
    public string DeactivationReason { get; set; } = null!;
    public bool IsUnDeactivated { get; set; } = false;
    public string? UnDeactivationReason { get; set; }
    public DateTime? UnDeactivationDate { get; set; }
}