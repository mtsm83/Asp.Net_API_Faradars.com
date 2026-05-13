using Faradars.Domain.BaseClasses;

namespace Faradars.Domain.Entities.Students.Interest;

public class InterestItem: BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    
    public ICollection<UserInterest> UserInterests { get; set; } = new List<UserInterest>();
}