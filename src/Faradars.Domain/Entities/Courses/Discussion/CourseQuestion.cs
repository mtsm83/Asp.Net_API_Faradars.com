using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Courses.Media;
using Faradars.Domain.Entities.Users.Admin;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Interfaces;

namespace Faradars.Domain.Entities.Courses.Discussion;

public class CourseQuestion : BaseEntity, IAdminInterference
{
    // todo: every question/review should be neither accepted or rejected before being visible to everyone

    public int CourseId { get; set; }
    public int LessonId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public int? AssetFileId { get; set; }

    // Admin Interference Properties
    public bool? IsAccepted { get; set; } = true;
    public string? RejectionCause { get; set; }
    public DateTime? RejectionDate { get; set; }
    public int? RelatedAdminId { get; set; }

    public User User { get; set; } = null!;
    public Lesson Lesson { get; set; } = null!;
    public Admin RelatedAdmin { get; set; } = null!;
    public AssetFile AssetFile { get; set; } = null!;
    public ICollection<QuestionAnswer> Answers { get; set; } = new List<QuestionAnswer>();

}