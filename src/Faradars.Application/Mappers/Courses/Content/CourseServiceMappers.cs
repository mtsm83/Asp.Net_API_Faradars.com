using Faradars.Application.DTOs.Courses.Content.CourseService;
using Faradars.Domain.Entities.Courses.Category;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Courses.Discussion;
using Faradars.Domain.Entities.Courses.Pricing.Bundle;
using Faradars.Domain.Entities.Courses.Pricing.Offer;
using Faradars.Domain.Entities.Courses.Tag;
using Faradars.Domain.Entities.Payments.Cart;
using Faradars.Domain.Entities.Students.Interest;
using Faradars.Domain.Enums;

namespace Faradars.Application.Mappers.Courses.Content;

public static class CourseServiceMappers
{
  public static void MapAddDtoToCourse(this Course course, AddCourseDto dto)
{
    // Map Level enum from string
    course.Level = dto.Level.ToLower() switch
    {
        "advanced" => CourseLevel.Advanced,
        "intermediate" => CourseLevel.Intermediate,
        "beginner" => CourseLevel.Beginner,
        "alllevels" => CourseLevel.AllLevels,
        _ => CourseLevel.Beginner // Default value
    };
    
    // Map TargetAudience enum from string
    course.TargetAudience = dto.TargetAudience.ToLower() switch
    {
        "infant" => TargetAudience.Infant,
        "toddler" => TargetAudience.Toddler,
        "preschooler" => TargetAudience.Preschooler,
        "child" => TargetAudience.Child,
        "adolescent" or "teenager" => TargetAudience.Adolescent,
        "youngadult" => TargetAudience.YoungAdult,
        "middleagedadult" => TargetAudience.MiddleAgedAdult,
        "senior" or "olderadult" => TargetAudience.Senior,
        _ => null // Allow null for TargetAudience
    };
    
    // Map CourseType enum from string (assuming dto has this)
    course.CourseType = dto.CourseType.ToLower() switch
    {
        "videobased" => CourseType.VideoBased,
        "audiobased" => CourseType.AudioBased,
        "textimagebased" => CourseType.TextImageBased,
        "all" => CourseType.All,
        _ => CourseType.VideoBased // Default
    };
    
    // Map primitive properties
    course.Title = dto.Title;
    course.Description = dto.Description;
    course.Language = dto.Language ?? "fa-IR";
    course.IsPublished = dto.IsPublished ?? true; // Only set once!
    course.TeacherId = dto.TeacherId;
    
    // Initialize collections (important!)
    course.Sections = new List<Section>();
    course.Lessons = new List<Lesson>();
    course.CourseCategories = new List<CourseCategory>();
    course.CourseTags = new List<CourseTag>();
    course.Images = new List<CourseImage>();
    course.CourseIntros = new List<CourseIntro>();
    course.Reviews = new List<CourseReview>();
    course.Questions = new List<CourseQuestion>();
    course.Answers = new List<QuestionAnswer>();
    course.WishlistItems = new List<WishlistItem>();
    course.OfferCourses = new List<OfferCourse>();
    course.BundleItems = new List<BundleCourse>();
    course.CartItems = new List<CartItem>();
    
    // Note: TotalDuration and AverageRating are calculated fields
    // They should NOT be set here
}
    public static void MapUpdateDtoToCourse(this Course course, UpdateCourseDto dto)
    {
        // Map Level enum from string
        course.Level = dto.Level.ToLower() switch
        {
            "advanced" => CourseLevel.Advanced,
            "intermediate" => CourseLevel.Intermediate,
            "beginner" => CourseLevel.Beginner,
            "alllevels" => CourseLevel.AllLevels,
            _ => CourseLevel.Beginner // Default value
        };
    
        // Map TargetAudience enum from string
        course.TargetAudience = dto.TargetAudience.ToLower() switch
        {
            "infant" => TargetAudience.Infant,
            "toddler" => TargetAudience.Toddler,
            "preschooler" => TargetAudience.Preschooler,
            "child" => TargetAudience.Child,
            "adolescent" or "teenager" => TargetAudience.Adolescent,
            "youngadult" => TargetAudience.YoungAdult,
            "middleagedadult" => TargetAudience.MiddleAgedAdult,
            "senior" or "olderadult" => TargetAudience.Senior,
            _ => null // Allow null for TargetAudience
        };
    
        // Map CourseType enum from string (assuming dto has this)
        course.CourseType = dto.CourseType.ToLower() switch
        {
            "videobased" => CourseType.VideoBased,
            "audiobased" => CourseType.AudioBased,
            "textimagebased" => CourseType.TextImageBased,
            "all" => CourseType.All,
            _ => CourseType.VideoBased // Default
        };
    
        // Map primitive properties
        course.Title = dto.Title;
        course.Description = dto.Description;
        course.Language = dto.Language ?? "fa-IR";
        course.IsPublished = dto.IsPublished ?? true; // Only set once!
        course.TeacherId = dto.TeacherId;
    }

    public static void MapToPreviewCourseDto(this Course course, PreviewCourseDto dto)
    {
        if (course == null) throw new ArgumentNullException(nameof(course));
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        dto.Id = course.Id;
        dto.Title = course.Title;
        dto.Description = course.Description;
        dto.Level = course.Level.ToString(); 
        dto.Language = course.Language;
        dto.IsPublished = course.IsPublished;
        dto.TotalDuration = course.TotalDuration;
        dto.AverageRating = course.AverageRating;
    }
    
    public static void MapToFullCourseDto(this Course course, FullCourseDto dto)
    {
        if (course == null) throw new ArgumentNullException(nameof(course));
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        dto.Id = course.Id;
        dto.Title = course.Title;
        dto.Description = course.Description;
        dto.Level = course.Level.ToString(); 
        dto.Language = course.Language;
        dto.IsPublished = course.IsPublished;
        dto.TotalDuration = course.TotalDuration;
        dto.AverageRating = course.AverageRating;
    }
}