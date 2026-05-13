using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Domain.Entities.Users.Role;

public class UserRole : BaseEntity
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}