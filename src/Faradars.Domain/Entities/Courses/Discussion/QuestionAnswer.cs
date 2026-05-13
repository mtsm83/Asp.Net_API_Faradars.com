using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Media;
using Faradars.Domain.Entities.Users.Admin;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Interfaces;

namespace Faradars.Domain.Entities.Courses.Discussion;

public class QuestionAnswer : BaseEntity, IAdminInterference
{
    public int QuestionId { get; set; }
    public string Body { get; set; } = null!;
    public int? AssetFileId { get; set; }

    // Admin Interference Properties
    public bool? IsAccepted { get; set; } = true;
    public string? RejectionCause { get; set; }
    public DateTime? RejectionDate { get; set; }
    public int? RelatedAdminId { get; set; }

    public User User { get; set; } = null!;
    public Admin Admin { get; set; } = null!;
    public CourseQuestion CourseQuestion { get; set; } = null!;
    public AssetFile AssetFile { get; set; } = null!;
}