using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Discussion;
using Faradars.Domain.Entities.Students.Enrollment;
using Faradars.Domain.Entities.Students.Interest;
using Faradars.Domain.Entities.Students.Subscription;
using Faradars.Domain.Entities.Users.Role;
using Faradars.Domain.Enums;

namespace Faradars.Domain.Entities.Users.Information;

public class User : BaseEntity
{
    public string FirstName { get; set; } // acts as the username for first time entrance
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public bool IsPhoneVerified { get; set; }
    public string? Email { get; set; }
    public bool IsEmailVerified { get; set; } 
    public DateOnly? BirthDate { get; set; }
    public GenderType? Gender { get; set; }
    public string? NCode { get; set; }
    public string Password { get; set; } = null!;

    #region Navigation Properties

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserInterest> UserInterests { get; set; } = new List<UserInterest>();
    public Teacher.Teacher TeacherProfile { get; set; } = null!;
    public ICollection<Enrollment> Taken { get; set; } = new List<Enrollment>();
    public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    public ICollection<CourseReview> Reviews { get; set; } = new List<CourseReview>();
    public ICollection<CourseQuestion> Questions { get; set; } = new List<CourseQuestion>();
    public ICollection<QuestionAnswer> Answers { get; set; } = new List<QuestionAnswer>();
    public ICollection<UserPlan> UserPlans { get; set; } = new List<UserPlan>();
    public ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();

    #endregion
}