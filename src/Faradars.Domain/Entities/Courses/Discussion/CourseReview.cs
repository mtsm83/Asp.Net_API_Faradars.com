using System.ComponentModel.DataAnnotations.Schema;
using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Users.Admin;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Interfaces;

namespace Faradars.Domain.Entities.Courses.Discussion;

public class CourseReview : BaseEntity, IAdminInterference
{
    public int CourseId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; } // todo: must be only in scope of [1,5]
    public string Body { get; set; } = null!;

    // Admin Interference Properties
    public bool? IsAccepted { get; set; } = true;
    public string? RejectionCause { get; set; }
    public DateTime? RejectionDate { get; set; }
    public int? RelatedAdminId { get; set; }

    public User User { get; set; } = null!;
    [ForeignKey("RelatedAdminId")] public Admin RelatedAdmin { get; set; } = null!;
    public Course Course { get; set; } = null!;
}