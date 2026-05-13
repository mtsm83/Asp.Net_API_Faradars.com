using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Courses.Media;

namespace Faradars.Domain.Entities.Users.Information;

public class UserProfile : BaseEntity
{
    public int UserId { get; set; }
    public int ProfileImageId { get; set; }

    public User User { get; set; } = null!;
    public AssetFile ProfileImage { get; set; } = null!;
}