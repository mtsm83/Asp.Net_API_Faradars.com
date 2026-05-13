using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Students.Subscription;
using Faradars.Domain.Entities.Users.Admin;
using Faradars.Domain.Enums;

namespace Faradars.Domain.Entities.Management.Request.Content;

public class UserRequest : BaseEntity
{
    public int UserId { get; set; }
    public int SubjectId { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Pending;
    public DateTime? CompletedAt { get; set; }
    public int? RelatedAdminId { get; set; }
    public string? RejectionCause { get; set; }
    
    public ICollection<RequestMessage> RequestMessages { get; set; } = new List<RequestMessage>();
    public ICollection<UserPlan> UserPlans { get; set; } = new List<UserPlan>();
    public RequestSubject Subject { get; set; } = null!;
    public Admin RelatedAdmin { get; set; } = null!;
}