using Faradars.Domain.BaseClasses;
using Faradars.Domain.Entities.Users.Information;

namespace Faradars.Domain.Entities.Users.Admin;

public class Admin : BaseEntity
{
    // Promoter = Creator, Note: nobody can be their own promoter!
    public int UserId { get; set; }
    public string? Description { get; set; }
    public string? Bio { get; set; }
    public string? LinkedinUrl { get; set; }
    
    public User User { get; set; } = null!;
    public Admin Promoter { get; set; } = null!;
}