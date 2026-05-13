using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Content;
using Faradars.Domain.Entities.Management.Request.Content;
using Faradars.Domain.Entities.Users.Information;
using Faradars.Domain.Enums;

namespace Faradars.Domain.Entities.Courses.Media;

public class AssetFile: BaseEntity
{
    public MediaType MediaType { get; set; } // video, image
    public ContentType ContentType { get; set; } // mp4 , jpeg
    public string OriginalFileName { get; set; } = null!;
    public string StoredFileName { get; set; } = null!;
    public long Size { get; set; } // 5MB to download
    public string Path { get; set; } = null!; // url
    public string? Description { get; set; }
    public string? Resolution { get; set; } // 1080p
    public TimeSpan? Duration { get; set; } 
    
    public ICollection<CourseImage> CourseImages { get; set; } = new List<CourseImage>();
    public ICollection<CourseIntro> CourseIntros { get; set; } = new List<CourseIntro>();
    public ICollection<MessageAsset> MessageAssets { get; set; } = new List<MessageAsset>();
    public ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
    public ICollection<LessonAsset> LessonAssets { get; set; } = new List<LessonAsset>();
    
    
}