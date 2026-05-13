using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Students.Enrollment;

namespace Faradars.Domain.Entities.Users.Teacher;

public class TeacherDismissal: BaseEntity
{
    // Disparager = Creator 
    public int TeacherId { get; set; }
    public string? DismissalReason { get; set; }
    public int DismissalTypeId { get; set; }

    public Teacher Teacher { get; set; } = null!;
    public Admin.Admin Disparager { get; set; } = null!;
    public DismissalType DismissalType { get; set; } = null!;
    
}