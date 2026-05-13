using System.ComponentModel.DataAnnotations.Schema;
using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Category;
using Faradars.Domain.Entities.Courses.Discussion;
using Faradars.Domain.Entities.Courses.Pricing.Bundle;
using Faradars.Domain.Entities.Courses.Pricing.Offer;
using Faradars.Domain.Entities.Courses.Subscription;
using Faradars.Domain.Entities.Courses.Tag;
using Faradars.Domain.Entities.Payments.Cart;
using Faradars.Domain.Entities.Students.Interest;
using Faradars.Domain.Entities.Users.Teacher;
using Faradars.Domain.Enums;

namespace Faradars.Domain.Entities.Courses.Content;

public class Course : BaseEntity
{
    // Must be calculated in its time:
    // AverageRating (After each Review inserted),
    // Price (after each Price inserted by Admin),
    // TotalDuration (after each fileAsset inserted)
    
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public CourseLevel Level { get; set; }
    public string Language { get; set; } = null!;
    public TargetAudience? TargetAudience { get; set; }
    public CourseType CourseType { get; set; }
    public bool IsPublished { get; set; }
    public TimeSpan? TotalDuration { get; set; } = null;
    public double AverageRating { get; set; } = 0;
    public int TeacherId { get; set; }
    
    [ForeignKey("TeacherId")] public Teacher Teacher { get; set; } = null!;
    [ForeignKey("RequiredPlanId")] public SubscriptionPlan Plan { get; set; } = null!;
    public ICollection<Section> Sections { get; set; } = new List<Section>();
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public ICollection<CourseCategory> CourseCategories { get; set; } = new List<CourseCategory>();
    public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
    public ICollection<CourseImage> Images { get; set; } = new List<CourseImage>();
    public ICollection<CourseIntro> CourseIntros { get; set; } = new List<CourseIntro>();
    public ICollection<CourseReview> Reviews { get; set; } = new List<CourseReview>();
    public ICollection<CourseQuestion> Questions { get; set; } = new List<CourseQuestion>();
    public ICollection<QuestionAnswer> Answers { get; set; } = new List<QuestionAnswer>();
    public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    public ICollection<OfferCourse> OfferCourses { get; set; } = new List<OfferCourse>();
    public ICollection<BundleCourse> BundleItems { get; set; } = new List<BundleCourse>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<SubscriptionCourse> SubscriptionCourses { get; set; } = new List<SubscriptionCourse>();
}