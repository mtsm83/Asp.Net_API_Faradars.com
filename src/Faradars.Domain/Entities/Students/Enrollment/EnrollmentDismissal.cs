using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Users.Admin;

namespace Faradars.Domain.Entities.Students.Enrollment;

public class EnrollmentDismissal : BaseEntity
{
    // Creator = RelatedAdmin
    public int EnrollmentId { get; set; }
    public int DismissalTypeId { get; set; }
    public string DismissalReason { get; set; } = null!;
    
    public Enrollment Enrollment { get; set; } = null!;
    public DismissalType DismissalType { get; set; } = null!;
    public Admin RelatedAdmin { get; set; } = null!;
    
}