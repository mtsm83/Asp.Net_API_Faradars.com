using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Students.Enrollment;

public class DismissalType : BaseEntity
{
    // "Violation_Instructors_Rules, Harassment"
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
}