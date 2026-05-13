using System.ComponentModel.DataAnnotations.Schema;
using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Students.Enrollment;

namespace Faradars.Domain.Entities.Users.Admin;

public class AdminDismissal : BaseEntity
{
    // DisparagerId = CreatedBy 
    public int AdminId { get; set; }
    public string? DismissalReason { get; set; }
    public int DismissalTypeId { get; set; }

    [ForeignKey("AdminId")] public Admin Admin { get; set; } = null!;
    [ForeignKey("CreatedBy")] public Admin Disparager { get; set; } = null!;
    public DismissalType DismissalType { get; set; } = null!;
    
}