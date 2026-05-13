using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Domain.Entities.Students.Interest;

public class UserInterest: BaseEntity
{
    public int UserId { get; set; }
    public int InterestId { get; set; }
    
    public User User { get; set; } = null!;
    public InterestItem InterestItem { get; set; } = null!;
}